namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class IdentificationType : EntityBase
{
    public string Name { get; set; } = null!;

    public string Abbreviation { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = [];
}