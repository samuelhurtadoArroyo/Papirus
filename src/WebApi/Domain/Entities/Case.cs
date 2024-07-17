using System.ComponentModel.DataAnnotations.Schema;

namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class Case : EntityBase
{
    public DateTime? RegistrationDate { get; set; }

    public Guid GuidIdentifier { get; set; }

    public string Court { get; set; } = null!;

    public string City { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime? SubmissionDate { get; set; }

    public string? SubmissionIdentifier { get; set; }

    public DateTime? DeadLineDate { get; set; }

    public int? ProcessTypeId { get; set; }

    public string? EmailHtmlBody { get; set; }

    public int? ProcessId { get; set; }

    public int? SubProcessId { get; set; }

    public string FilePath { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public bool IsAssigned { get; set; }

    public int? BusinessLineId { get; set; }

    public virtual ICollection<Actor> Actors { get; set; } = [];

    public virtual ICollection<CaseProcessDocument> CaseProcessDocuments { get; set; } = [];

    [Include]
    public virtual Assignment Assignment { get; set; } = null!;

    public virtual Process Process { get; set; } = null!;

    public virtual ProcessType ProcessType { get; set; } = null!;

    public virtual SubProcess? SubProcess { get; set; }

    public virtual BusinessLine? BusinessLine { get; set; }

    public bool? IsAnswered { get; set; } = false;

    public DateTime? AnsweredDate { get; set; }

    public bool? EmergencyBriefAnswered { get; set; } = false;

    public DateTime? EmergencyBriefAnswerDate { get; set; }

    [NotMapped]
    public bool IsCurrentAssigned { get; set; }
}