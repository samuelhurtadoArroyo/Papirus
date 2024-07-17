namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class PersonType : EntityBase
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = [];
}