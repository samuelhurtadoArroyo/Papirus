namespace Papirus.WebApi.Domain.Interfaces.Repositories;

public interface ICaseAssignmentRepository : IRepository<Assignment>
{
    Task AddAssignmentAsync(Assignment assignment);

    Task UpdateAssignmentAsync(Assignment assignment);

    Task<IEnumerable<Assignment>> GetAssignmentsByTeamMemberIdAsync(int teamMemberId);

    Task<Assignment?> GetAssignmentsByCaseIdAsync(int caseId);
}