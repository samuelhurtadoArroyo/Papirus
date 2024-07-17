namespace Papirus.WebApi.Infrastructure.Repositories;

public class ProcessTemplatesRepository : Repository<ProcessTemplate>, IProcessTemplatesRepository
{
    public ProcessTemplatesRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}