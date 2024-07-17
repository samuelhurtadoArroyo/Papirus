namespace Papirus.WebApi.Application.Services;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;

    public TeamService(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<Team> Create(Team model)
    {
        return await _teamRepository.AddAsync(model);
    }

    public async Task Delete(int id)
    {
        var original = await _teamRepository.GetByIdAsync(id);

        if (original is not null)
        {
            await _teamRepository.RemoveAsync(original);
            return;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<Team> Edit(Team model)
    {
        var id = model.Id;
        var original = await _teamRepository.GetByIdAsync(id);

        if (original is not null)
        {
            return await _teamRepository.UpdateAsync(model);
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<IEnumerable<Team>> GetAll()
    {
        return await _teamRepository.GetAllAsync();
    }

    public async Task<Team> GetById(int id)
    {
        var current = await _teamRepository.GetByIdAsync(id);

        if (current is not null)
        {
            return current;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<QueryResult<Team>> GetByQueryRequestAsync(QueryRequest queryRequest)
    {
        return await _teamRepository.GetByQueryRequestAsync(queryRequest);
    }
}