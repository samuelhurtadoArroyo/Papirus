namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class Person : EntityBase
{
    public Guid GuidIdentifier { get; set; }

    public int PersonTypeId { get; set; }

    public string Name { get; set; } = null!;

    public int IdentificationTypeId { get; set; }

    public string IdentificationNumber { get; set; } = null!;

    public string SupportFileName { get; set; } = null!;

    public string SupportFilePath { get; set; } = null!;

    public virtual ICollection<Actor> Actors { get; set; } = [];

    public virtual IdentificationType IdentificationType { get; set; } = null!;

    public virtual PersonType PersonType { get; set; } = null!;
}