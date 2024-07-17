namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class Assignment : EntityBase
{
    public int TeamMemberId { get; set; }

    public int CaseId { get; set; }

    public int StatusId { get; set; }

    public virtual Case Case { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    [Include]
    public virtual TeamMember TeamMember { get; set; } = null!;
}