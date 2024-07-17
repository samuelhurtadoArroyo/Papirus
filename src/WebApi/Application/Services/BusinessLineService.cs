namespace Papirus.WebApi.Application.Services;

public class IBussinessLineService : IBusinessLineService
{
    private readonly IBusinessLineRepository _repository;

    private readonly IMapper _mapper;

    public IBussinessLineService(IBusinessLineRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BusinessLineDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<BusinessLineDto>>(entities);
    }
}