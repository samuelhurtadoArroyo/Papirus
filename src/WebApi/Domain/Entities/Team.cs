namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class Team : EntityBase
{
    public string Name { get; set; } = null!;

    public virtual ICollection<TeamMember> TeamMembers { get; set; } = [];
}