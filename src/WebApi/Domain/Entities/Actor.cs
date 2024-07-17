namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class Actor : EntityBase
{
    public int ActorTypeId { get; set; }

    public int PersonId { get; set; }

    public int CaseId { get; set; }

    public virtual ActorType ActorType { get; set; } = null!;

    public virtual Case Case { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
}