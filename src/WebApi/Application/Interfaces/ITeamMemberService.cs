
namespace Papirus.WebApi.Application.Interfaces;

public interface ITeamMemberService : IService<TeamMember>
{
    public Task<IEnumerable<TeamMember>> GetByTeamId(int teamId);
}
