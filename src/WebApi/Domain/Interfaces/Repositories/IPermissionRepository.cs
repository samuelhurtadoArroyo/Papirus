namespace Papirus.WebApi.Domain.Interfaces.Repositories;

public interface IPermissionRepository : IRepository<Permission>
{
    Task<IEnumerable<Permission>> GetAllByUser(int userId);
}