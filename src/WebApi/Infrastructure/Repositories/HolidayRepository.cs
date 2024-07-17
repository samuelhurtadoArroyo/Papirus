namespace Papirus.WebApi.Infrastructure.Repositories;

public class HolidayRepository : Repository<Holiday>, IHolidayRepository
{
    public HolidayRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}