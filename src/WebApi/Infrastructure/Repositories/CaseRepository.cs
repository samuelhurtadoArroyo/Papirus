namespace Papirus.WebApi.Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
public class CaseRepository : Repository<Case>, ICaseRepository
{
    private readonly AppDbContext _appDbContext;

    public CaseRepository(AppDbContext appDbContext) : base(appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<Case>> GetGuardianshipsWithDetailsAsync()
    {
        return await _appDbContext.Cases
            .Include(c => c.Actors)
                .ThenInclude(a => a.Person)
            .Include(c => c.Assignment)
                .ThenInclude(a => a.TeamMember)
                    .ThenInclude(tm => tm.Member)
            .Include(c => c.Assignment)
                .ThenInclude(a => a.Status)
            .ToListAsync();
    }

    public async Task<QueryResult<Case>> GetByQueryRequestIncludingAsync(QueryRequest queryRequest)
    {
        var query = _appDbContext.Cases
            .Include(c => c.Actors)
                .ThenInclude(a => a.Person)
            .Include(c => c.Assignment)
                .ThenInclude(a => a.TeamMember)
                    .ThenInclude(tm => tm.Member)
            .Include(c => c.Assignment)
                .ThenInclude(a => a.Status);
        return await GetByQueryRequestIncludingAsync(query, queryRequest);
    }
}