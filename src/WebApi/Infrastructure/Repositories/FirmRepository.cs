namespace Papirus.WebApi.Infrastructure.Repositories;

public class FirmRepository : Repository<Firm>, IFirmRepository
{
    public FirmRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}