using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class CaseDtoBuilder
{
    private int _id;

    private Guid _guidIdentifier;

    private DateTime? _registrationDate;

    private string? _court;

    private string? _city;

    private decimal? _amount;

    private DateTime? _submissionDate;

    private string? _submissionIdentifier;

    private DateTime? _deadLineDate;

    private ProcessTypeId _processTypeId;

    private int? _processId;

    private int? _subProcessId;

    private string? _filePath;

    private string? _fileName;

    private bool? _isAssigned;

    private bool? _isAnswered;

    private DateTime? _answeredDate;

    public CaseDtoBuilder()
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
        _isAnswered = false;
        _answeredDate = null!;
    }

    public CaseDtoBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public CaseDtoBuilder WithGuidIdentifier(Guid guidIdentifier)
    {
        _guidIdentifier = guidIdentifier;
        return this;
    }

    public CaseDtoBuilder WithRegistrationDate(DateTime? registrationDate)
    {
        _registrationDate = registrationDate;
        return this;
    }

    public CaseDtoBuilder WithCourt(string? court)
    {
        _court = court;
        return this;
    }

    public CaseDtoBuilder WithCity(string? city)
    {
        _city = city;
        return this;
    }

    public CaseDtoBuilder WithAmount(decimal? amount)
    {
        _amount = amount;
        return this;
    }

    public CaseDtoBuilder WithSubmissionDate(DateTime? submissionDate)
    {
        _submissionDate = submissionDate;
        return this;
    }

    public CaseDtoBuilder WithSubmissionIdentifier(string? submissionIdentifier)
    {
        _submissionIdentifier = submissionIdentifier;
        return this;
    }

    public CaseDtoBuilder WithDeadLineDate(DateTime? deadlineDate)
    {
        _deadLineDate = deadlineDate;
        return this;
    }

    public CaseDtoBuilder WithProcessTypeId(ProcessTypeId processTypeId)
    {
        _processTypeId = processTypeId;
        return this;
    }

    public CaseDtoBuilder WithProcessId(int? processId)
    {
        _processId = processId;
        return this;
    }

    public CaseDtoBuilder WithSubProcessId(int? subProcessId)
    {
        _subProcessId = subProcessId;
        return this;
    }

    public CaseDtoBuilder WithFilePath(string? filePath)
    {
        _filePath = filePath;
        return this;
    }

    public CaseDtoBuilder WithFileName(string? fileName)
    {
        _fileName = fileName;
        return this;
    }

    public CaseDtoBuilder WithIsAssigned(bool? isAssigned)
    {
        _isAssigned = isAssigned;
        return this;
    }

    public CaseDtoBuilder WithIsAnswered(bool? isAnswered)
    {
        _isAnswered = isAnswered;
        return this;
    }

    public CaseDtoBuilder WithAnswereDate(DateTime? answerDate)
    {
        _answeredDate = answerDate;
        return this;
    }

    public CaseDto Build()
    {
        return new CaseDto
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
            ProcessTypeId = _processTypeId,
            ProcessId = _processId,
            SubProcessId = _subProcessId,
            FilePath = _filePath,
            FileName = _fileName,
            IsAssigned = _isAssigned,
            IsAnswered = _isAnswered,
            AnsweredDate = _answeredDate
        };
    }
}