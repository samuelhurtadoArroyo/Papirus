namespace Papirus.WebApi.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Role> Create(Role model)
    {
        return await _roleRepository.AddAsync(model);
    }

    public async Task Delete(int id)
    {
        var original = await _roleRepository.GetByIdAsync(id);

        if (original is not null)
        {
            await _roleRepository.RemoveAsync(original);
            return;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<Role> Edit(Role model)
    {
        var id = model.Id;
        var original = await _roleRepository.GetByIdAsync(id);

        if (original is not null)
        {
            return await _roleRepository.UpdateAsync(model);
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<IEnumerable<Role>> GetAll()
    {
        return await _roleRepository.GetAllAsync();
    }

    public async Task<Role> GetById(int id)
    {
        var current = await _roleRepository.GetByIdAsync(id);

        if (current is not null)
        {
            return current;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<QueryResult<Role>> GetByQueryRequestAsync(QueryRequest queryRequest)
    {
        return await _roleRepository.GetByQueryRequestAsync(queryRequest);
    }
}