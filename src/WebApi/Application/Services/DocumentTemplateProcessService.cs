using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using Papirus.WebApi.Application.Common.Utilities;
using System.Text;

namespace Papirus.WebApi.Application.Services;

[ExcludeFromCodeCoverage]
public class DocumentTemplateProcessService : IDocumentTemplateProcessService
{
    private readonly ICaseProcessDocumentService _caseProcessDocumentService;

    private readonly IProcessTemplatesService _processTemplatesService;

    private readonly ICaseDocumentFieldValueService _caseDocumentFieldValueService;

    private readonly ICaseService _caseService;

    private readonly IBusinessLineService _lineService;

    private readonly IEnumerable<BusinessLineDto> _businessLines;

    private readonly IDataManagerService _dataManagerService;

    private readonly IMapper _mapper;

    private string _baseContainerName = "GomezPineda/Tutelas";

    public DocumentTemplateProcessService(
        ICaseProcessDocumentService caseProcessDocumentService,

        IProcessTemplatesService processTemplatesService,
        ICaseDocumentFieldValueService caseDocumentFieldValueService,
        ICaseService caseService,
        IBusinessLineService businessLineService,
        IDataManagerService dataManagerService,
        IMapper mapper)
    {
        _processTemplatesService = processTemplatesService;
        _caseDocumentFieldValueService = caseDocumentFieldValueService;
        _caseService = caseService;
        _lineService = businessLineService;
        _businessLines = GetBusinessLines().Result;
        _caseProcessDocumentService = caseProcessDocumentService;
        _dataManagerService = dataManagerService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CaseDocumentFieldValueDto>> GetTemplateDictionaryAsync(int caseId)
    {
        var caseDocumentFieldValues = await _caseDocumentFieldValueService.GetByCaseIdAsync(caseId);

        return await Task.FromResult(caseDocumentFieldValues);
    }

    protected static void DeleteWordTemporaryFiles()
    {
        string[] wordFilesX = Directory.GetFiles(System.IO.Path.GetTempPath(), "*.docx");

        string[] allWordFiles = new string[wordFilesX.Length];

        wordFilesX.CopyTo(allWordFiles, wordFilesX.Length);

        foreach (string file in allWordFiles)
        {
            File.Delete(file);
        }
    }

    public async Task<IEnumerable<CaseProcessDocument>> GetDocumentProcessAsync(int caseId)
    {
        var item = _caseProcessDocumentService.GetByCaseId(caseId);
        return await Task.FromResult(item.Result);
    }

    public string MoveTemplate(string fileNameOrigin, byte[] contentResult)
    {
        string newPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), fileNameOrigin);
        File.WriteAllBytes(newPath, contentResult);

        return newPath;
    }

    protected static string MoveTemplate(string path, string fileNameOrigin)
    {
        string destinationFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), fileNameOrigin);
        File.Copy(path, destinationFile, true);

        return destinationFile;
    }

    private string GetTemplateLocation(string rootDirectory, string fileDirectory, string fileName)
    {
        var directories = Directory.GetDirectories(rootDirectory, "*", SearchOption.AllDirectories);
        string rutaArchivo = string.Empty;

        directories = directories.Append(fileDirectory).ToArray();

        foreach (var directory in directories)
        {
            if (directory.Contains("Templates"))
            {
                var filePath = System.IO.Path.Combine(directory, fileName);
                if (File.Exists(filePath))
                {
                    rutaArchivo = System.IO.Path.Combine(directory, fileName);
                }
            }
        }
        return rutaArchivo;
    }

    public async Task<CaseProcessDocumentDto> ReplaceTextAsync(IEnumerable<CaseDocumentFieldValueDto> caseDocumentFieldValues, ProcessTemplate processtemplate, int caseId, int documentType, string rootDirectory)
    {
        string fileTemplate = GetTemplateLocation(rootDirectory, processtemplate.FilePath, processtemplate.FileName);
        string newFilePath = MoveTemplate(fileTemplate, processtemplate.FileName);
        var _case = await _caseService.GetById(caseId);
        var lineaBusines = _businessLines.First(x => x.Id == _case.BusinessLineId);
        List<CaseDocumentFieldValueDto> fieldvaluesList =
        [
            .. caseDocumentFieldValues,
            new()
            {
                CaseId = caseId,
                Tag = "[Banco]",
                FieldValue = _case.BusinessLineId != 1 ? lineaBusines.Name : string.Empty,
                Id = caseDocumentFieldValues.Count() + 1
            },
        ];

        // Load the document using NPOI
        await using (FileStream stream = new(newFilePath, FileMode.Open, FileAccess.ReadWrite))
        {
            XWPFDocument document = new(stream);

            foreach (var paragraph in document.Paragraphs)
            {
                foreach (var fieldValue in fieldvaluesList)
                {
                    ReplaceTextInParagraph(paragraph, fieldValue.Tag, fieldValue.FieldValue);
                }
            }

            // Save the changes to the document by writing it to a MemoryStream
            await using MemoryStream memoryStream = new();
            document.Write(memoryStream);
            File.WriteAllBytes(newFilePath, memoryStream.ToArray());
        }

        await UpdateCase(caseId, documentType);

        var processDocument = await SaveCaseprocessDocument(_dataManagerService, processtemplate.FileName, caseId, documentType, newFilePath);

        return await Task.FromResult(_mapper.Map<CaseProcessDocument, CaseProcessDocumentDto>(processDocument));
    }

    private void ReplaceTextInParagraph(string oldValue, XWPFParagraph paragraph, string newValue)
    {
        string text = paragraph.ParagraphText;

        // Remove tab characters
        string modifiedText = text.Replace("\t", "");
        string replacedText = text.Replace(oldValue, newValue);
        // Clear the runs in the paragraph
        while (paragraph.Runs.Count > 0)
        {
            paragraph.RemoveRun(0);
        }

        // Add the modified text back as a single run
        XWPFRun run = paragraph.CreateRun();
        run.SetText(replacedText);
    }

    private void ReplaceTextInParagraph(XWPFParagraph paragraph, string placeholder, string newValue)
    {
        // Use FastReplacer for efficient token replacement
        var fastReplacer = new FastReplacer("[", "]");

        // Collect text and runs
        var paragraphText = new StringBuilder();
        var runsToKeep = new List<XWPFRun>();
        var runsToRemove = new List<int>();

        foreach (var run in paragraph.Runs)
        {
            // Check if the run is a Field or Hyperlink run and skip it
            if (run is XWPFHyperlinkRun || run.GetCTR().Items.OfType<CT_FldChar>().Any())
            {
                runsToKeep.Add(run);
                continue;
            }

            string runText = run.GetText(0);
            if (!string.IsNullOrEmpty(runText))
            {
                paragraphText.Append(runText);
            }
            runsToRemove.Add(paragraph.Runs.IndexOf(run));
        }

        // Append text to FastReplacer
        fastReplacer.Append(paragraphText.ToString());

        // Replace the placeholder using FastReplacer
        fastReplacer.Replace(placeholder, newValue);

        // Get the replaced text
        string replacedText = fastReplacer.ToString();

        // Remove the existing runs that are not Field or Hyperlink runs
        foreach (int index in runsToRemove.OrderByDescending(i => i))
        {
            paragraph.RemoveRun(index);
        }

        // Add back the preserved Field or Hyperlink runs
        foreach (var run in runsToKeep)
        {
            XWPFRun newRun = paragraph.CreateRun();
            CopyRunProperties(run, newRun);
            newRun.SetText(run.GetText(0), 0);
        }

        // Create new runs with the replaced text
        if (!string.IsNullOrEmpty(replacedText))
        {
            // Split the replaced text into multiple runs if needed
            XWPFRun newRun = paragraph.CreateRun();
            newRun.SetText(replacedText);
        }
    }

    private void CopyRunProperties(XWPFRun sourceRun, XWPFRun destinationRun)
    {
        destinationRun.FontSize = sourceRun.FontSize;
        destinationRun.FontFamily = sourceRun.FontFamily;
        destinationRun.Underline = sourceRun.Underline;
        destinationRun.SetStyle(sourceRun.GetStyle());
    }

    private async Task<CaseProcessDocument> SaveCaseprocessDocument(IDataManagerService dataManagerService, string fileName, int caseId, int documentType, string newFilePth)
    {
        byte[] contentDocument = File.ReadAllBytes(newFilePth);
        Guid guidIdentifier = _caseService.GetById(caseId).Result.GuidIdentifier;
        var containerName = $"{_baseContainerName}/{guidIdentifier}";
        var url = dataManagerService.UploadFileAsync(new()
        {
            Content = contentDocument,
            FileName = fileName
        }, containerName);

        CaseProcessDocument caseProcessDocumentDto = new()
        {
            FileName = fileName,
            FilePath = url.Result,
            CaseId = caseId,
            DocumentTypeId = documentType
        };
        return await PostDocumentProcessAsync(caseProcessDocumentDto);
    }

    public async Task<CaseProcessDocument> PostDocumentProcessAsync(CaseProcessDocument caseProcessDocument)
    {
        return await _caseProcessDocumentService.Create(caseProcessDocument);
    }

    public async Task<ProcessTemplate> GetProcessTemplateDocumentAsync(int caseId, int templateId)
    {
        var processtEmplate = await _processTemplatesService.GetAll();
        var caseItem = await _caseService.GetById(caseId);

        return processtEmplate.FirstOrDefault(x => x.Id == templateId)!;
    }

    private async Task<IEnumerable<BusinessLineDto>> GetBusinessLines()
    {
        return await _lineService.GetAllAsync();
    }

    private async Task<Case> UpdateCase(int caseId, int documentType)
    {
        var caseItem = await _caseService.GetById(caseId);

        if (documentType == 22)
        {
            caseItem.IsAnswered = true;
            caseItem.AnsweredDate = DateTime.Now;
        }
        else if (documentType == 23)
        {
            caseItem.EmergencyBriefAnswered = true;
            caseItem.EmergencyBriefAnswerDate = DateTime.Now;
        }
        return await _caseService.Edit(caseItem);
    }

    public void DeleteTempFiles()
    {
        if (Directory.Exists(System.IO.Path.GetTempPath()))
        {
            var wordFiles = Directory.GetFiles(System.IO.Path.GetTempPath(), "*.docx");
            foreach (var file in wordFiles)
            {
                try
                {
                    File.Delete(file);
                }
                catch (IOException ex)
                {
                    throw new InternalServerErrorException(ex.Message);
                }
            }
        }
    }
}