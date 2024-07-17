namespace Papirus.WebApi.Application.Interfaces;

public interface ICaseService : IService<Case>
{
    Task<Case> UpdateBusinessLineAsync(int id, int businessLineId);

    Task<Case> GetCaseByIdWithAssignmentAsync(int id);

    Task<IEnumerable<Case>> GetCaseAllWithAssignmentAsync();

    Task<List<GuardianshipDto>> GetGuardianshipsAsync();

    Task<QueryResult<Case>> GetGuardianshipsAsync(QueryRequest queryRequest);
}