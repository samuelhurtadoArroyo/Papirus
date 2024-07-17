namespace Papirus.WebApi.Application.Services;

public class ActorService : IActorService
{
    private readonly IActorRepository _actorRepository;

    public ActorService(IActorRepository actorRepository)
    {
        _actorRepository = actorRepository;
    }

    public async Task<Actor> Create(Actor model)
    {
        return await _actorRepository.AddAsync(model);
    }

    public async Task Delete(int id)
    {
        var original = await _actorRepository.GetByIdAsync(id);

        if (original is not null)
        {
            await _actorRepository.RemoveAsync(original);
            return;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<Actor> Edit(Actor model)
    {
        var id = model.Id;
        var original = await _actorRepository.GetByIdAsync(id);

        if (original is not null)
        {
            return await _actorRepository.UpdateAsync(model);
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<IEnumerable<Actor>> GetAll()
    {
        return await _actorRepository.GetAllAsync();
    }

    public async Task<Actor> GetById(int id)
    {
        var current = await _actorRepository.GetByIdAsync(id);

        if (current is not null)
        {
            return current;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    [ExcludeFromCodeCoverage]
    public async Task<QueryResult<Actor>> GetByQueryRequestAsync(QueryRequest queryRequest)
    {
        return await _actorRepository.GetByQueryRequestAsync(queryRequest);
    }
}