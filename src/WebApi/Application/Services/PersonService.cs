namespace Papirus.WebApi.Application.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<Person> Create(Person model)
    {
        return await _personRepository.AddAsync(model);
    }

    public async Task Delete(int id)
    {
        var original = await _personRepository.GetByIdAsync(id);

        if (original is not null)
        {
            await _personRepository.RemoveAsync(original);
            return;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<Person> Edit(Person model)
    {
        var id = model.Id;
        var original = await _personRepository.GetByIdAsync(id);

        if (original is not null)
        {
            return await _personRepository.UpdateAsync(model);
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<IEnumerable<Person>> GetAll()
    {
        return await _personRepository.GetAllAsync();
    }

    public async Task<Person> GetById(int id)
    {
        var current = await _personRepository.GetByIdAsync(id);

        if (current is not null)
        {
            return current;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<QueryResult<Person>> GetByQueryRequestAsync(QueryRequest queryRequest)
    {
        return await _personRepository.GetByQueryRequestAsync(queryRequest);
    }
}