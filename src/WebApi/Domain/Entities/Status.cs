namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class Status : EntityBase
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Assignment> Assignments { get; set; } = [];
}