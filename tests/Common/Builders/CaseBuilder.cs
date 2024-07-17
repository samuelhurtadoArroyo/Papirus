using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class CaseBuilder
{
    private int _id;

    private Guid _guidIdentifier;

    private DateTime? _registrationDate;

    private string _court;

    private string _city;

    private decimal _amount;

    private DateTime? _submissionDate;

    private string? _submissionIdentifier;

    private DateTime? _deadLineDate;

    private ProcessTypeId _processTypeId;

    private string? _emailHtmlBody;

    private int _processId;

    private int? _subProcessId;

    private string _filePath;

    private string _fileName;

    private bool _isAssigned;

    private bool? _isAnswered;

    private DateTime? _answeredDate;

    public CaseBuilder()
    {
        _id = 0;
        _guidIdentifier = Guid.NewGuid();
        _registrationDate = null!;
        _court = null!;
        _city = null!;
        _amount = 0;
        _submissionDate = null!;
        _submissionIdentifier = null!;
        _deadLineDate = null!;
        _processTypeId = ProcessTypeId.Demand;
        _processId = 0;
        _subProcessId = null!;
        _filePath = null!;
        _fileName = null!;
        _isAssigned = false;
        _emailHtmlBody = null!;
        _isAnswered = false;
        _answeredDate = null;
    }

    public CaseBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public CaseBuilder WithGuidIdentifier(Guid guidIdentifier)
    {
        _guidIdentifier = guidIdentifier;
        return this;
    }

    public CaseBuilder WithRegistrationDate(DateTime? registrationDate)
    {
        _registrationDate = registrationDate;
        return this;
    }

    public CaseBuilder WithCourt(string court)
    {
        _court = court;
        return this;
    }

    public CaseBuilder WithCity(string city)
    {
        _city = city;
        return this;
    }

    public CaseBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public CaseBuilder WithSubmissionDate(DateTime? submissionDate)
    {
        _submissionDate = submissionDate;
        return this;
    }

    public CaseBuilder WithSubmissionIdentifier(string? submissionIdentifier)
    {
        _submissionIdentifier = submissionIdentifier;
        return this;
    }

    public CaseBuilder WithDeadLineDate(DateTime? deadlineDate)
    {
        _deadLineDate = deadlineDate;
        return this;
    }

    public CaseBuilder WithProcessTypeId(ProcessTypeId processTypeId)
    {
        _processTypeId = processTypeId;
        return this;
    }

    public CaseBuilder WithProcessId(int processId)
    {
        _processId = processId;
        return this;
    }

    public CaseBuilder WithSubProcessId(int? subProcessId)
    {
        _subProcessId = subProcessId;
        return this;
    }

    public CaseBuilder WithEmailHtmlBody(string? emailHtmlBody)
    {
        _emailHtmlBody = emailHtmlBody;
        return this;
    }

    public CaseBuilder WithFilePath(string filePath)
    {
        _filePath = filePath;
        return this;
    }

    public CaseBuilder WithFileName(string fileName)
    {
        _fileName = fileName;
        return this;
    }

    public CaseBuilder WithIsAssigned(bool isAssigned)
    {
        _isAssigned = isAssigned;
        return this;
    }

    public CaseBuilder WithIsAnswered(bool? isAnswered)
    {
        _isAnswered = isAnswered;
        return this;
    }
    public CaseBuilder WithAnswereDate(DateTime? answerDate)
    {
        _answeredDate = answerDate;
        return this;
    }


    public Case Build()
    {
        return new Case
        {
            Id = _id,
            GuidIdentifier = _guidIdentifier,
            RegistrationDate = _registrationDate,
            Court = _court,
            City = _city,
            Amount = _amount,
            SubmissionDate = _submissionDate,
            SubmissionIdentifier = _submissionIdentifier,
            DeadLineDate = _deadLineDate,
            ProcessTypeId = (int)_processTypeId,
            EmailHtmlBody = _emailHtmlBody,
            ProcessId = _processId,
            SubProcessId = _subProcessId,
            FilePath = _filePath,
            FileName = _fileName,
            IsAssigned = _isAssigned
        };
    }
}