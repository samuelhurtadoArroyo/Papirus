namespace Papirus.WebApi.Application.Services;

[ExcludeFromCodeCoverage]
public class CaseService : ICaseService
{
    private readonly ICaseRepository _caseRepository;

    private readonly IMapper _mapper;

    private readonly ICurrentUserService _currentUserService;

    public CaseService(ICaseRepository caseRepository, IMapper mapper, ICurrentUserService currentUserService)
    {
        _caseRepository = caseRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<Case> Create(Case model)
    {
        return await _caseRepository.AddAsync(model);
    }

    public async Task Delete(int id)
    {
        var existingCase = await _caseRepository.GetByIdAsync(id);

        if (existingCase is not null)
        {
            await _caseRepository.RemoveAsync(existingCase);
            return;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<Case> Edit(Case model)
    {
        var id = model.Id;
        var existingCase = await _caseRepository.GetByIdAsync(id);

        if (existingCase is not null)
        {
            return await _caseRepository.UpdateAsync(model);
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<IEnumerable<Case>> GetAll()
    {
        return await _caseRepository.GetAllAsync();
    }

    public async Task<Case> GetById(int id)
    {
        var existingCase = await _caseRepository.GetByIdAsync(id);

        if (existingCase is not null)
        {
            return existingCase;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<QueryResult<Case>> GetByQueryRequestAsync(QueryRequest queryRequest)
    {
        return await _caseRepository.GetByQueryRequestAsync(queryRequest);
    }

    public async Task<Case> UpdateBusinessLineAsync(int id, int businessLineId)
    {
        var existingCase = await _caseRepository.GetByIdAsync(id);

        if (existingCase is not null)
        {
            existingCase.BusinessLineId = businessLineId;
            return await _caseRepository.UpdateAsync(existingCase);
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<Case> GetCaseByIdWithAssignmentAsync(int id)
    {
        var existingCase = await _caseRepository.GetByIdIncludingAsync(id, x => x.Assignment, x => x.Assignment.Status, x => x.Assignment.TeamMember.Member);

        if (existingCase is not null)
        {
            return existingCase;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<IEnumerable<Case>> GetCaseAllWithAssignmentAsync()
    {
        return await _caseRepository.GetAllIncludingAsync(x => x.Assignment);
    }

    public async Task<List<GuardianshipDto>> GetGuardianshipsAsync()
    {
        var cases = await _caseRepository.GetGuardianshipsWithDetailsAsync();
        return _mapper.Map<List<GuardianshipDto>>(cases);
    }

    public async Task<QueryResult<Case>> GetGuardianshipsAsync(QueryRequest queryRequest)
    {
        var cases = await _caseRepository.GetByQueryRequestIncludingAsync(queryRequest);
        var currentUserId = await _currentUserService.GetCurrentUserIdAsync();
        foreach (var item in cases.Items)
            item.IsCurrentAssigned = item.Assignment?.TeamMember?.Member?.Id == currentUserId;

        return _mapper.Map<QueryResult<Case>>(cases);
    }
}