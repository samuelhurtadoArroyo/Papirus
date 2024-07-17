namespace Papirus.WebApi.Application.Services;

public class TeamMemberService : ITeamMemberService
{
    private readonly ITeamMemberRepository _teamMemberRepository;

    public TeamMemberService(ITeamMemberRepository teamMemberRepository)
    {
        _teamMemberRepository = teamMemberRepository;
    }

    public async Task<TeamMember> Create(TeamMember model)
    {
        return await _teamMemberRepository.AddAsync(model);
    }

    public async Task Delete(int id)
    {
        var teamMember = await _teamMemberRepository.GetByIdAsync(id);

        if (teamMember is not null)
        {
            await _teamMemberRepository.RemoveAsync(teamMember);
            return;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<TeamMember> Edit(TeamMember model)
    {
        var id = model.Id;
        var teamMember = await _teamMemberRepository.GetByIdAsync(id);

        if (teamMember is not null)
        {
            return await _teamMemberRepository.UpdateAsync(model);
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<IEnumerable<TeamMember>> GetAll()
    {
        return await _teamMemberRepository.GetAllIncludingAsync(tm => tm.Member);
    }

    public async Task<TeamMember> GetById(int id)
    {
        var teamMember = await _teamMemberRepository.GetByIdIncludingAsync(id, tm => tm.Member);

        if (teamMember is not null)
        {
            return teamMember;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<QueryResult<TeamMember>> GetByQueryRequestAsync(QueryRequest queryRequest)
    {
        return await _teamMemberRepository.GetByQueryRequestAsync(queryRequest);
    }

    public async Task<IEnumerable<TeamMember>> GetByTeamId(int teamId)
    {
        return await _teamMemberRepository.GetByTeamIdAsync(teamId);
    }
}