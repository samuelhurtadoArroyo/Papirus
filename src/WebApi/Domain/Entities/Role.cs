namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class Role : EntityBase
{
    public string Name { get; set; } = null!;

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = [];

    public virtual ICollection<User> Users { get; set; } = [];
}