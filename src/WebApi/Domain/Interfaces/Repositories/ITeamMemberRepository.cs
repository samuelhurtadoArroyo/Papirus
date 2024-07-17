namespace Papirus.WebApi.Domain.Interfaces.Repositories;

public interface ITeamMemberRepository : IRepository<TeamMember>
{
    Task<IEnumerable<TeamMember>> GetByTeamIdAsync(int teamId);

    Task<TeamMember> GetTeamMemberByIdAsync(int id);

    Task<IEnumerable<TeamMember>> GetAllTeamMembersAsync();

    Task AddTeamMemberAsync(TeamMember teamMember);

    Task UpdateTeamMemberAsync(TeamMember teamMember);

    Task DeleteTeamMemberAsync(int id);

    Task<IEnumerable<TeamMember>> GetTeamMembersByTeamIdAsync(int teamId);
}