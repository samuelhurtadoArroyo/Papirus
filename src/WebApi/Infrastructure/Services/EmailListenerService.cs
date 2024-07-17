using Microsoft.Extensions.DependencyInjection;
using Papirus.WebApi.Application.Common.Models;
using Papirus.WebApi.Application.Dtos.DataExtractor;
using Papirus.WebApi.Domain.Define.Enums;
using Papirus.WebApi.Infrastructure.Common.Mappings;

namespace Papirus.WebApi.Infrastructure.Services;

[ExcludeFromCodeCoverage]
public class EmailListenerService : BackgroundService
{
    private readonly ILogger<EmailListenerService> _logger;

    private readonly IEmailAuthenticationService _authenticationService;

    private readonly IHtmlToTextConverter _htmlToTextConverter;

    private readonly IAttachmentExtractor _attachmentExtractor;

    private readonly IServiceScopeFactory _serviceScopeFactory;

    private readonly EmailOptions _emailOptions;

    private readonly ImapClient _client;

    private CancellationTokenSource? _doneToken;

    private bool _needToCheckMessages;

    private readonly string _baseContainerName = "GomezPineda/Tutelas";

    private readonly string _folderToProcessPath = "Inbox/GomezPinedaAbogados";

    private readonly string _processedFolderPath = "Inbox/GomezPinedaAbogados/Processed";
    private readonly int _retries = 1;

    public EmailListenerService(
        ILogger<EmailListenerService> logger,
        IEmailAuthenticationService authenticationService,
        IHtmlToTextConverter htmlToTextConverter,
        IAttachmentExtractor attachmentExtractor,
        IServiceScopeFactory serviceScopeFactory,
        IOptions<EmailOptions> emailOptions)
    {
        _logger = logger;
        _authenticationService = authenticationService;
        _htmlToTextConverter = htmlToTextConverter;
        _attachmentExtractor = attachmentExtractor;
        _serviceScopeFactory = serviceScopeFactory;
        _emailOptions = emailOptions.Value;
        _client = new ImapClient(new ProtocolLogger(Console.OpenStandardError()));
        _needToCheckMessages = true;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await ReconnectAsync(stoppingToken);

            var folder = await _client.GetFolderAsync(_folderToProcessPath, stoppingToken);
            await folder.OpenAsync(FolderAccess.ReadWrite, stoppingToken);
            folder.MessageFlagsChanged += OnMessageFlagsChanged;
            await IdleClientAsync(stoppingToken);
        }
        finally
        {
            _client.Inbox.MessageFlagsChanged -= OnMessageFlagsChanged;
            await _client.DisconnectAsync(true, stoppingToken);
            _client.Dispose();
        }
    }

    private async Task ReconnectAsync(CancellationToken cancellationToken)
    {
        if (!_client.IsConnected)
            await _client.ConnectAsync(_emailOptions.ImapServer, _emailOptions.ImapPort, SecureSocketOptions.SslOnConnect, cancellationToken);
        if (!_client.IsAuthenticated)
        {
            var accessToken = await _authenticationService.AcquireTokenAsync();
            await _client.AuthenticateAsync(new SaslMechanismOAuth2(_emailOptions.UserName, accessToken), cancellationToken);
        }
    }

    private async Task IdleClientAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _doneToken = new CancellationTokenSource();
            try
            {
                var idleTask = _client.IdleAsync(_doneToken.Token, stoppingToken);
                var delayTask = Task.Delay(_emailOptions.IdleCheckInterval, stoppingToken);

                await Task.WhenAny(idleTask, delayTask);

                if (!idleTask.IsCompleted)
                {
                    await _doneToken.CancelAsync();
                    await idleTask;
                }

                if (_needToCheckMessages || idleTask.IsCompletedSuccessfully)
                {
                    var folder = await _client.GetFolderAsync(_folderToProcessPath, stoppingToken);
                    await folder.OpenAsync(FolderAccess.ReadWrite, stoppingToken);

                    var uids = await folder.SearchAsync(SearchQuery.NotSeen, stoppingToken);
                    foreach (var uid in uids)
                    {
                        var message = await folder.GetMessageAsync(uid, stoppingToken);
                        bool processed = false;
                        for (int i = 0; i < _retries; i++)
                        {
                            processed = await ProcessEmailAsync(message.ConvertToEmailMessage());
                            if (processed) break;
                        }
                        
                        await folder.AddFlagsAsync(uid, MessageFlags.Seen, true, stoppingToken);

                        if (processed)
                        {
                            var processedFolderPath = _client.GetFolder(_processedFolderPath);
                            await folder.MoveToAsync(uid, processedFolderPath);
                        }
                    }

                    _needToCheckMessages = false;
                }
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogError(ex, "Operation cancelled with exception: {ex}", ex.Message);
            }
            finally
            {
                _doneToken.Dispose();
            }
        }
    }

    private void OnMessageFlagsChanged(object? sender, EventArgs e)
    {
        _needToCheckMessages = true;
        _doneToken?.Cancel();
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping Email Listener Service.");
        _doneToken?.CancelAsync();
        await base.StopAsync(cancellationToken);
    }

    private string ExtractEmailContent(EmailMessage message)
    {
        if (!string.IsNullOrEmpty(message.HtmlBody))
        {
            return _htmlToTextConverter.ConvertHtmlToPlainText(message.HtmlBody);
        }

        if (!string.IsNullOrEmpty(message.TextBody))
        {
            return message.TextBody;
        }

        return string.Empty;
    }

    private async Task<bool> ProcessEmailAsync(EmailMessage message)
    {
        _logger.LogInformation("Processing email from: {From}", message.From);

        try
        {
            string plainText = ExtractEmailContent(message);
            if (string.IsNullOrEmpty(plainText))
            {
                _logger.LogInformation("PlainText extracted from email was null or empty for message with subject: {subject} and coming from: {from}", message.Subject, message.From);

                return false;
            }
            plainText = _emailOptions.EmailBodyIdentification + plainText;

            var attachments = _attachmentExtractor.GetAllAttachments(message).ToList();

            var autoAdmiteAttachment = attachments.Find(a => IsAutoAdmiteDocument(a.FileName));
            if (autoAdmiteAttachment is null)
            {
                string attachmentsAsString = string.Join(" | ", attachments.Select(x => x.FileName));

                _logger.LogInformation("Could not find auto admite attachments in any of the attachments: {attachments} for message with subject: {subject} and coming from: {from}",
                    attachmentsAsString, message.Subject, message.From);

                return false;
            }
            var guidIdentifier = Guid.NewGuid();
            var containerName = $"{_baseContainerName}/{guidIdentifier}";
            attachments.Remove(autoAdmiteAttachment!);

            using var scope = _serviceScopeFactory.CreateScope();
            var dataExtractorService = scope.ServiceProvider.GetRequiredService<IDataExtractorService>();
            var dataManagerService = scope.ServiceProvider.GetRequiredService<IDataManagerService>();
            var caseService = scope.ServiceProvider.GetRequiredService<ICaseService>();
            var caseDocumentsService = scope.ServiceProvider.GetRequiredService<ICaseProcessDocumentService>();
            var actorService = scope.ServiceProvider.GetRequiredService<IActorService>();
            var personRepository = scope.ServiceProvider.GetRequiredService<IPersonRepository>();
            var deadlineDateService = scope.ServiceProvider.GetRequiredService<IDeadLineDateService>();
            var caseDocumentFieldValuesRepository = scope.ServiceProvider.GetRequiredService<ICaseDocumentFieldValueRepository>();

            var emailBodyFileName = "EmailBody.txt";
            var textBytes = System.Text.Encoding.Unicode.GetBytes(plainText);

            var autoAdmiteUrl = await UploadAutoAdmite(dataManagerService, autoAdmiteAttachment, containerName);
            var emailBodyUrl = await UploadEmailBody(dataManagerService, emailBodyFileName, textBytes, containerName);

            var autoAdmiteInfo = await ExtractDocumentInfo(dataExtractorService, autoAdmiteUrl, FormatType.Pdf);

            var emailBodyInfo = await ExtractDocumentInfo(dataExtractorService, emailBodyUrl, FormatType.Text);

            var newCase = await CreateCase(caseService, deadlineDateService, autoAdmiteInfo, emailBodyInfo, message, autoAdmiteUrl, autoAdmiteAttachment.FileName, guidIdentifier);
            var newPerson = await CreatePerson(personRepository, emailBodyInfo, emailBodyUrl, emailBodyFileName);

            await AddAttachmentsToCase(caseDocumentFieldValuesRepository, autoAdmiteInfo, emailBodyInfo, caseDocumentsService,
                attachments, dataManagerService, newCase, autoAdmiteUrl, autoAdmiteAttachment.FileName, emailBodyUrl, emailBodyFileName, containerName);
            await AddActorsToCase(actorService, personRepository, newCase, newPerson);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing email from {From}: {Message}", message.From, ex.Message);

            return false;
        }
    }

    private static async Task<string> UploadAutoAdmite(IDataManagerService dataManagerService, FileAttachment autoAdmiteAttachment, string containerName) =>
        await dataManagerService.UploadFileAsync(autoAdmiteAttachment, containerName);

    private static async Task<string> UploadEmailBody(IDataManagerService dataManagerService, string emailBodyFileName, byte[] textBytes, string containerName) =>
        await dataManagerService.UploadFileAsync(new()
        {
            Content = textBytes,
            FileName = emailBodyFileName
        }, containerName);

    private static async Task<ResultDto> ExtractDocumentInfo(IDataExtractorService dataExtractorService, string documentUrl, FormatType formatType)
    {
        var document = new DocumentToProcessDto
        {
            DocumentUrl = documentUrl,
            FileType = formatType
        };
        return await dataExtractorService.ProcessDocumentAsync(document);
    }

    private async Task<Case> CreateCase(ICaseService caseService, IDeadLineDateService deadLineDateService, ResultDto autoAdmiteInfo, ResultDto emailBodyInfo, EmailMessage message, string autoAdmiteUrl, string autoAdmiteFileName, Guid guidIdentifier)
    {
        string registrationDateString = emailBodyInfo.DocumentFields?.Find(x => x.DocumentFieldName.Equals(EmailBodyFieldNames.FechaAsignacion))?.FieldValue ?? "";
        string submissionDateString = emailBodyInfo.DocumentFields?.Find(x => x.DocumentFieldName.Equals(EmailBodyFieldNames.FechaNotificacion))?.FieldValue ?? "";
        int termHours = ParseDeadlineHours(autoAdmiteInfo.DocumentFields?.Find(x => x.DocumentFieldName.Equals(AutoAdmiteFieldNames.TerminoDeVencimiento))?.FieldValue);

        if (!TryParseDateWithLanguageDetection(registrationDateString, out DateTime registrationDate)) registrationDate = DateTime.MinValue;
        if (!TryParseDateWithLanguageDetection(submissionDateString, out DateTime submissionDate)) submissionDate = DateTime.MinValue;

        var newCase = new Case
        {
            RegistrationDate = registrationDate == DateTime.MinValue ? null : registrationDate,
            GuidIdentifier = guidIdentifier,
            Court = autoAdmiteInfo.DocumentFields?.Find(x => x.DocumentFieldName.Equals(AutoAdmiteFieldNames.Juzgado))?.FieldValue ?? "",
            City = emailBodyInfo.DocumentFields?.Find(x => x.DocumentFieldName.Equals(EmailBodyFieldNames.Ciudad))?.FieldValue ?? "",
            Amount = 0.0m,
            SubmissionDate = submissionDate == DateTime.MinValue ? null : submissionDate,
            SubmissionIdentifier = autoAdmiteInfo.DocumentFields?.Find(x => x.DocumentFieldName.Equals(AutoAdmiteFieldNames.Radicado))?.FieldValue ?? "",
            DeadLineDate = submissionDate == DateTime.MinValue ? null : deadLineDateService.CalculateDeadlineAsync(submissionDate, termHours),
            ProcessTypeId = 2,
            EmailHtmlBody = message.HtmlBody,
            ProcessId = 8,
            SubProcessId = null,
            FilePath = autoAdmiteUrl,
            FileName = autoAdmiteFileName,
            IsAssigned = false,
            BusinessLineId = null
        };

        return await caseService.Create(newCase);
    }

    private static async Task<Person> CreatePerson(IPersonRepository personRepository, ResultDto emailBodyInfo, string emailBodyUrl, string emailBodyFileName)
    {
        var existingPerson = await FindPerson(personRepository, emailBodyInfo);

        if (existingPerson is not null) return existingPerson;

        var person = new Person
        {
            GuidIdentifier = Guid.NewGuid(),
            PersonTypeId = (int)PersonTypeId.Natural,
            Name = emailBodyInfo.DocumentFields?.Find(x => x.DocumentFieldName.Equals(EmailBodyFieldNames.Accionante))?.FieldValue ?? "",
            IdentificationTypeId = (int)IdentificationTypeId.CitizenshipId,
            IdentificationNumber = emailBodyInfo.DocumentFields?.Find(x => x.DocumentFieldName.Equals(EmailBodyFieldNames.IdentificacionAccionante))?.FieldValue ?? "",
            SupportFilePath = emailBodyUrl,
            SupportFileName = emailBodyFileName
        };

        return await personRepository.AddAsync(person);
    }

    private static async Task<Person?> FindPerson(IPersonRepository personRepository, ResultDto emailBodyInfo)
    {
        var idNumber = emailBodyInfo.DocumentFields?.FirstOrDefault(x =>
            x.DocumentFieldName.Equals(EmailBodyFieldNames.IdentificacionAccionante))?.FieldValue;

        if (string.IsNullOrEmpty(idNumber))
        {
            return await FindPersonByName(personRepository, emailBodyInfo);
        }

        var personByIdNumber = await personRepository.FindByIdentificationAsync(IdentificationTypeId.CitizenshipId, idNumber);

        return personByIdNumber ?? await FindPersonByName(personRepository, emailBodyInfo);
    }

    private static async Task<Person?> FindPersonByName(IPersonRepository personRepository, ResultDto emailBodyInfo)
    {
        var name = emailBodyInfo.DocumentFields?.FirstOrDefault(x =>
            x.DocumentFieldName.Equals(AutoAdmiteFieldNames.Accionante))?.FieldValue;

        return string.IsNullOrEmpty(name)
            ? null
            : (await personRepository.FindAsync(x =>
                x.Name.ToLower() == name.ToLower())).FirstOrDefault();
    }


    private static async Task AddAttachmentsToCase(ICaseDocumentFieldValueRepository caseDocumentFieldValueRepository, ResultDto autoAdmite, ResultDto emailBody,
                                                   ICaseProcessDocumentService caseDocumentsService, List<FileAttachment> attachments, IDataManagerService dataManagerService,
                                                   Case newCase, string autoAdmiteUrl, string autoAdmiteFileName, string emailBodyUrl, string emailBodyFileName, string containerName)
    {
        foreach (var attachment in attachments)
        {
            var filePath = await dataManagerService.UploadFileAsync(attachment, containerName);

            await caseDocumentsService.Create(new()
            {
                DocumentTypeId = 20,
                CaseId = newCase.Id,
                FilePath = filePath,
                FileName = attachment.FileName
            });
        }

        var autoAdmiteCaseDocument = await caseDocumentsService.Create(new()
        {
            DocumentTypeId = 20,
            CaseId = newCase.Id,
            FilePath = autoAdmiteUrl,
            FileName = autoAdmiteFileName
        });

        foreach (var item in autoAdmite.DocumentFields!)
        {
            await caseDocumentFieldValueRepository.AddAsync(
                new CaseDocumentFieldValue()
                {
                    CaseProcessDocumentId = autoAdmiteCaseDocument.Id,
                    DocumentTypeId = 20, // Tutela
                    //ProcessDocumentTypeId = 1,
                    CaseId = newCase.Id,
                    Name = item.DocumentFieldName,
                    Tag = $"[{item.DocumentFieldName
                                .Replace(" ", "")
                                .Replace("/", "")
                                .Replace("_", "")
                                .Replace("-", "")}]",
                    //Multiplicity = 1,
                    FieldValue = item.FieldValue
                });
        }

        foreach (var item in emailBody.DocumentFields!)
        {
            await caseDocumentFieldValueRepository.AddAsync(
                new CaseDocumentFieldValue()
                {
                    CaseProcessDocumentId = autoAdmiteCaseDocument.Id,
                    DocumentTypeId = 21, // TutelaEmailBody
                    //ProcessDocumentTypeId = 1,
                    CaseId = newCase.Id,
                    Name = item.DocumentFieldName,
                    Tag = $"[{item.DocumentFieldName
                                .Replace(" ", "")
                                .Replace("/", "")
                                .Replace("_", "")
                                .Replace("-", "")}]",
                    //Multiplicity = 1,
                    FieldValue = item.FieldValue
                });
        }

        await caseDocumentsService.Create(new()
        {
            DocumentTypeId = 20,
            CaseId = newCase.Id,
            FilePath = emailBodyUrl,
            FileName = emailBodyFileName
        });
    }

    private static async Task AddActorsToCase(IActorService actorService, IPersonRepository personRepository, Case newCase, Person newPerson)
    {
        var defendant = await personRepository.FindByIdentificationAsync(IdentificationTypeId.TaxIdentificationNumber, "890903938-8");

        await actorService.Create(new()
        {
            ActorTypeId = (int)ActorTypeId.Claimer,
            PersonId = newPerson.Id,
            CaseId = newCase.Id
        });

        await actorService.Create(new()
        {
            ActorTypeId = (int)ActorTypeId.Defendant,
            PersonId = defendant!.Id,
            CaseId = newCase.Id
        });
    }

    private bool IsAutoAdmiteDocument(string fileName)
        => _emailOptions.AutoAdmiteKeywords.Exists(keyword => fileName.Contains(keyword, StringComparison.OrdinalIgnoreCase));

    private static bool TryParseDateWithLanguageDetection(string dateString, out DateTime result)
    {
        if (string.IsNullOrEmpty(dateString))
        {
            result = DateTime.MinValue;

            return false;
        }

        string[] spanishMonths = { "enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre" };
        string[] spanishFormats = {
        "dddd, d 'de' MMMM 'de' yyyy HH:mm",
        "dddd, d 'de' MMMM 'de' yyyy H:mm",
        "dddd, d 'de' MMMM 'de' yyyy hh:mm tt",
        "dddd, d 'de' MMMM 'de' yyyy h:mm tt",
        "d 'de' MMMM 'de' yyyy HH:mm",
        "d 'de' MMMM 'de' yyyy H:mm",
        "d 'de' MMMM 'de' yyyy hh:mm tt",
        "d 'de' MMMM 'de' yyyy h:mm tt"
        };

        string[] englishFormats = {
        "dddd, MMMM d, yyyy HH:mm",
        "dddd, MMMM d, yyyy H:mm",
        "dddd, MMMM d, yyyy hh:mm tt",
        "dddd, MMMM d, yyyy h:mm tt",
        "MMMM d, yyyy HH:mm",
        "MMMM d, yyyy H:mm",
        "MMMM d, yyyy hh:mm tt",
        "MMMM d, yyyy h:mm tt"
        };

        bool isSpanish = spanishMonths.Any(month => dateString.Contains(month, StringComparison.OrdinalIgnoreCase));

        if (isSpanish)
        {
            dateString = dateString.Replace(" AM", " a. m.").Replace(" PM", " p. m.")
                                   .Replace(" a.m.", " a. m.").Replace(" p.m.", " p. m.")
                                   .Replace("sabado", "sábado").Replace("miercoles", "miércoles");
            return TryParseExactDate(dateString, spanishFormats, new CultureInfo("es-ES"), out result);
        }
        else
        {
            return TryParseExactDate(dateString, englishFormats, new CultureInfo("en-US"), out result);
        }
    }

    private static bool TryParseExactDate(string dateString, string[] formats, CultureInfo culture, out DateTime result)
    {
        foreach (var format in formats)
        {
            if (DateTime.TryParseExact(dateString, format, culture, DateTimeStyles.None, out result))
                return true;
        }
        result = DateTime.MinValue;
        return false;
    }

    private int ParseDeadlineHours(string? deadlineString)
    {
        if (string.IsNullOrEmpty(deadlineString))
        {
            _logger.LogInformation("Deadline string from autoadmite is null or empty, value: {deadlineString}", deadlineString);

            return 0;
        }
        var deadlineParts = deadlineString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (deadlineParts.Length != 2 || !int.TryParse(deadlineParts[0], out int number))
        {
            _logger.LogInformation("Deadline string from autoadmite is unprocessable, value: {deadlineString}", deadlineString);

            return 0;
        }

        var unit = deadlineParts[1].Trim().ToUpperInvariant();

        if (unit == "DIAS") return number * 24;
        else if (unit == "HORAS") return number;

        _logger.LogInformation("Could not find the time measurement for deadline date, value: {deadlineString}", deadlineString);

        return 0;
    }
}