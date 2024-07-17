namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class TeamMember : EntityBase
{
    public int TeamId { get; set; }

    public int MemberId { get; set; }

    public bool IsLead { get; set; }

    public int MaxCases { get; set; }

    public int AssignedCases { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = [];

    public virtual User Member { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}