namespace Papirus.WebApi.Application.Dtos;

[ExcludeFromCodeCoverage]
public class GuardianshipDto
{
    /// <summary>
    /// The unique identifier for the guardianship case.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The date when the guardianship was submitted.
    /// </summary>
    public DateTime? SubmissionDate { get; set; }

    /// <summary>
    /// The deadline date for processing the guardianship.
    /// </summary>
    public DateTime? DeadLineDate { get; set; }

    /// <summary>
    /// The name of the defendant in the guardianship case.
    /// </summary>
    public string DefendantName { get; set; } = string.Empty;

    /// <summary>
    /// The name of the claimant in the guardianship case.
    /// </summary>
    public string ClaimerName { get; set; } = string.Empty;

    /// <summary>
    /// A unique identifier for the guardianship submission.
    /// </summary>
    public string SubmissionIdentifier { get; set; } = string.Empty;

    /// <summary>
    /// The current status of the guardianship.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// The current status of the guardianship.
    /// </summary>
    public int StatusId { get; set; }

    /// <summary>
    /// The team member id assigned to this guardianship.
    /// </summary>
    public int AssignedTeamMemberId { get; set; }

    /// <summary>
    /// The user id assigned to this guardianship.
    /// </summary>
    public int MemberId { get; set; }

    /// <summary>
    /// The team member name assigned to this guardianship.
    /// </summary>
    public string AssignedTeamMemberName { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the case has been assigned.
    /// </summary>
    public bool? IsAssigned { get; set; }

    /// <summary>
    /// flag to know if the document has been answered
    /// </summary>
    public bool? IsAnswered { get; set; } = false;

    /// <summary>
    /// Date when the document has been answered
    /// </summary>
    public DateTime? AnsweredDate { get; set; }

    /// <summary>
    /// Indicate if the user is the current assigned team member.
    /// </summary>
    public bool IsCurrentAssigned { get; set; }

    /// <summary>
    /// Indicate if was answered
    /// </summary>
    public bool? EmergencyBriefAnswered { get; set; } = false;

    /// <summary>
    /// Date when the brief answered was sent
    /// </summary>
    public DateTime? EmergencyBriefAnswerDate { get; set; }
}