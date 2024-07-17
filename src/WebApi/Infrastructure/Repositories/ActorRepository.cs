namespace Papirus.WebApi.Infrastructure.Repositories;

public class ActorRepository : Repository<Actor>, IActorRepository
{
    public ActorRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}