namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class Firm : EntityBase
{
    public string Name { get; set; } = null!;

    public Guid GuidIdentifier { get; set; }

    public virtual ICollection<ProcessTemplate> ProcessTemplates { get; set; } = [];

    public virtual ICollection<User> Users { get; set; } = [];
}