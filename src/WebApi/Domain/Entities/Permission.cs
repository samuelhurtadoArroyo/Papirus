namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class Permission : EntityBase
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string PermissionLabelCode { get; set; } = null!;

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = [];
}