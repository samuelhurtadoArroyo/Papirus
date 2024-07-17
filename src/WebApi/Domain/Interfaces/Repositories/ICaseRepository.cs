namespace Papirus.WebApi.Domain.Interfaces.Repositories;

public interface ICaseRepository : IRepository<Case>
{
    Task<List<Case>> GetGuardianshipsWithDetailsAsync();

    Task<QueryResult<Case>> GetByQueryRequestIncludingAsync(QueryRequest queryRequest);
}