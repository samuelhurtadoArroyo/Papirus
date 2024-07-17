namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class RolePermission : EntityBase
{
    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}