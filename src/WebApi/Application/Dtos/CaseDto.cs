using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.WebApi.Application.Dtos;

/// <summary>
/// Represents a case within the system
/// </summary>
[ExcludeFromCodeCoverage]
public class CaseDto
{
    /// <summary>
    /// The unique identifier for the case.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The registration date of the case.
    /// </summary>
    public DateTime? RegistrationDate { get; set; }

    /// <summary>
    /// A unique GUID identifier for the case.
    /// </summary>
    public Guid GuidIdentifier { get; set; }

    /// <summary>
    /// The court handling the case.
    /// </summary>
    public string? Court { get; set; }

    /// <summary>
    /// The city where the case is registered.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// The amount involved in the case.
    /// </summary>
    public decimal? Amount { get; set; }

    /// <summary>
    /// The date the case was submitted.
    /// </summary>
    public DateTime? SubmissionDate { get; set; }

    /// <summary>
    /// A unique identifier for the case submission.
    /// </summary>
    public string? SubmissionIdentifier { get; set; }

    /// <summary>
    /// The deadline date for case processing.
    /// </summary>
    public DateTime? DeadLineDate { get; set; }

    /// <summary>
    /// The type of process the case is involved in.
    /// </summary>
    public ProcessTypeId? ProcessTypeId { get; set; }

    /// <summary>
    /// The email's html body.
    /// </summary>
    public string? EmailHtmlBody { get; set; }

    /// <summary>
    /// The identifier for the process this case is associated with.
    /// </summary>
    public int? ProcessId { get; set; }

    /// <summary>
    /// The identifier for the subprocess this case is associated with.
    /// </summary>
    public int? SubProcessId { get; set; }

    /// <summary>
    /// The file path to documents associated with the case.
    /// </summary>
    public string? FilePath { get; set; }

    /// <summary>
    /// The name of the file associated with the case.
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// Indicates whether the case has been assigned.
    /// </summary>
    public bool? IsAssigned { get; set; }

    /// <summary>
    /// Business line id associated with the case.
    /// </summary>
    public int? BusinessLineId { get; set; }

    /// <summary>
    /// flag to know if the document has been answered
    /// </summary>
    public bool? IsAnswered { get; set; } = false;

    /// <summary>
    /// Date when the document has been answered
    /// </summary>
    public DateTime? AnsweredDate { get; set; }

    /// <summary>
    /// flag to know where the Emergency brief has been answered
    /// </summary>
    public bool? EmergencyBriefAnswered { get; set; } = false;

    /// <summary>
    /// Date when the Emergency Brief has been answered
    /// </summary>
    public DateTime? EmergencyBriefAnswerDate { get; set; }
}