namespace Papirus.WebApi.Infrastructure.Repositories;

public class CaseProcessDocumentRepository : Repository<CaseProcessDocument>, ICaseProcessDocumentRepository
{
    public CaseProcessDocumentRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}