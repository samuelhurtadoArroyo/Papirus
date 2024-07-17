namespace Papirus.WebApi.Application.Interfaces;

public interface ICaseAssignmentService
{
    public Task<IEnumerable<Assignment>> GetAssignmentsByTeamIdAsync(int teamId);

    public Task<CaseAssignmentDto> AssignCaseToTeamMember(int caseId, int userId, int caseStatusId);

    public Task<List<TeamMemberAssignmentDto>> GetTeamMembersIfLead(int teamMemberId);
}