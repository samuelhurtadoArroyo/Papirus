namespace Papirus.WebApi.Infrastructure.Repositories;

public class ProcessDocumentTypeRepository : Repository<ProcessDocumentType>, IProcessDocumentTypeRepository
{
    public ProcessDocumentTypeRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}