namespace Papirus.WebApi.Infrastructure.Repositories;

public class TeamRepository : Repository<Team>, ITeamRepository
{
    public TeamRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}