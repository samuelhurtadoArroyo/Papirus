namespace Papirus.WebApi.Application.Services;

public class ProcessDocumentTypesService : IProcessDocumentTypesService
{
    private readonly IProcessDocumentTypeRepository _processDocumentTypeRepository;

    public ProcessDocumentTypesService(IProcessDocumentTypeRepository repository)
    {
        _processDocumentTypeRepository = repository;
    }

    public async Task<ProcessDocumentType> Create(ProcessDocumentType model)
    {
        return await _processDocumentTypeRepository.AddAsync(model);
    }

    public async Task Delete(int id)
    {
        var retrievedProcess = await _processDocumentTypeRepository.GetByIdAsync(id);

        if (retrievedProcess is not null)
        {
            await _processDocumentTypeRepository.RemoveAsync(retrievedProcess);
            return;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<ProcessDocumentType> Edit(ProcessDocumentType model)
    {
        var id = model.Id;
        var retrievedProcess = await _processDocumentTypeRepository.GetByIdAsync(id);

        if (retrievedProcess is not null)
        {
            return await _processDocumentTypeRepository.UpdateAsync(model);
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<IEnumerable<ProcessDocumentType>> GetAll()
    {
        return await _processDocumentTypeRepository.GetAllAsync();
    }

    public async Task<ProcessDocumentType> GetById(int id)
    {
        var retrievedProcess = await _processDocumentTypeRepository.GetByIdAsync(id);

        if (retrievedProcess is not null)
        {
            return retrievedProcess;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    [ExcludeFromCodeCoverage]
    public Task<QueryResult<ProcessDocumentType>> GetByQueryRequestAsync(QueryRequest queryRequest)
    {
        throw new NotImplementedException();
    }
}