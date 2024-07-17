namespace Papirus.WebApi.Application.Services;

public class FirmService : IFirmService
{
    private readonly IFirmRepository _firmRepository;

    public FirmService(IFirmRepository firmRepository)
    {
        _firmRepository = firmRepository;
    }

    public async Task<Firm> Create(Firm model)
    {
        return await _firmRepository.AddAsync(model);
    }

    public async Task Delete(int id)
    {
        var original = await _firmRepository.GetByIdAsync(id);

        if (original is not null)
        {
            await _firmRepository.RemoveAsync(original);
            return;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<Firm> Edit(Firm model)
    {
        var id = model.Id;
        var original = await _firmRepository.GetByIdAsync(id);

        if (original is not null)
        {
            return await _firmRepository.UpdateAsync(model);
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<IEnumerable<Firm>> GetAll()
    {
        return await _firmRepository.GetAllAsync();
    }

    public async Task<Firm> GetById(int id)
    {
        var current = await _firmRepository.GetByIdAsync(id);

        if (current is not null)
        {
            return current;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<QueryResult<Firm>> GetByQueryRequestAsync(QueryRequest queryRequest)
    {
        return await _firmRepository.GetByQueryRequestAsync(queryRequest);
    }
}