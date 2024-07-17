namespace Papirus.WebApi.Infrastructure.Repositories;

public class PermissionRepository : Repository<Permission>, IPermissionRepository
{
    private readonly AppDbContext _appDbContext;

    public PermissionRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<Permission>> GetAllByUser(int userId)
    {
        return await _appDbContext
            .Permissions
            .Where(d => d.RolePermissions.Any(x => x.Role.Users.Any(u => u.Id == userId)))
            .ToListAsync();
    }
}