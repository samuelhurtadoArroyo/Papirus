namespace Papirus.WebApi.Application.Services;

public class ProcessTemplatesService : IProcessTemplatesService
{
    private readonly IProcessTemplatesRepository _processTemplatesRepository;

    public ProcessTemplatesService(IProcessTemplatesRepository processTemplatesRepository)
    {
        _processTemplatesRepository = processTemplatesRepository;
    }

    public async Task<ProcessTemplate> Create(ProcessTemplate model)
    {
        return await _processTemplatesRepository.AddAsync(model);
    }

    public async Task Delete(int id)
    {
        var existingProcessTemplate = await _processTemplatesRepository.GetByIdAsync(id);
        if (existingProcessTemplate != null)
        {
            await _processTemplatesRepository.RemoveAsync(existingProcessTemplate);
            return;
        }
        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<ProcessTemplate> Edit(ProcessTemplate model)
    {
        var id = model.Id;
        var existingProcessTemplate = await _processTemplatesRepository.GetByIdAsync(id);

        if (existingProcessTemplate is not null)
        {
            return await _processTemplatesRepository.UpdateAsync(model);
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<IEnumerable<ProcessTemplate>> GetAll()
    {
        return await _processTemplatesRepository.GetAllAsync();
    }

    public async Task<ProcessTemplate> GetById(int id)
    {
        return await _processTemplatesRepository.GetByIdAsync(id);
    }

    [ExcludeFromCodeCoverage]
    public Task<QueryResult<ProcessTemplate>> GetByQueryRequestAsync(QueryRequest queryRequest)
    {
        throw new NotImplementedException();
    }
}