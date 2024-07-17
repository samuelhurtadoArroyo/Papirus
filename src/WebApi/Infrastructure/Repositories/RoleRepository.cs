namespace Papirus.WebApi.Infrastructure.Repositories;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}