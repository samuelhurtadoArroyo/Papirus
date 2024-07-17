namespace Papirus.WebApi.Infrastructure.Repositories;

public class BusinessLineRepository : Repository<BusinessLine>, IBusinessLineRepository
{
    public BusinessLineRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}