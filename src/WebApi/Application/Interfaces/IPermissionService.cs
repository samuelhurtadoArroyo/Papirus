namespace Papirus.WebApi.Application.Interfaces;

public interface IPermissionService : IService<Permission>
{
    Task<IEnumerable<Permission>> GetAllPermissionsByUserAsync();

    Task<bool> HasPermission(int userId, string permission);
}