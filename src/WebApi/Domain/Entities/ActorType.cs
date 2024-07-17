namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class ActorType : EntityBase
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Actor> Actors { get; set; } = [];
}