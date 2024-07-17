namespace Papirus.WebApi.Application.Services;

public class PermissionService : IPermissionService
{
    private readonly IPermissionRepository _permissionRepository;

    private readonly ICurrentUserService _currentUserService;

    public PermissionService(IPermissionRepository permissionRepository, ICurrentUserService currentUserService)
    {
        _permissionRepository = permissionRepository;
        _currentUserService = currentUserService;
    }

    public async Task<Permission> Create(Permission model)
    {
        return await _permissionRepository.AddAsync(model);
    }

    public async Task Delete(int id)
    {
        var original = await _permissionRepository.GetByIdAsync(id);

        if (original is not null)
        {
            await _permissionRepository.RemoveAsync(original);
            return;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<Permission> Edit(Permission model)
    {
        var id = model.Id;
        var original = await _permissionRepository.GetByIdAsync(id);

        if (original is not null)
        {
            return await _permissionRepository.UpdateAsync(model);
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<IEnumerable<Permission>> GetAll()
    {
        return await _permissionRepository.GetAllAsync();
    }

    public async Task<Permission> GetById(int id)
    {
        var current = await _permissionRepository.GetByIdAsync(id);

        if (current is not null)
        {
            return current;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<QueryResult<Permission>> GetByQueryRequestAsync(QueryRequest queryRequest)
    {
        return await _permissionRepository.GetByQueryRequestAsync(queryRequest);
    }

    public async Task<IEnumerable<Permission>> GetAllPermissionsByUserAsync()
    {
        return await _permissionRepository.GetAllByUser(await _currentUserService.GetCurrentUserIdAsync());
    }

    public async Task<bool> HasPermission(int userId, string permission)
    {
        var permissions = await _permissionRepository.FindAsync(d => d.RolePermissions.Any(e => e.Permission.PermissionLabelCode == permission) && d.RolePermissions.Any(e => e.Role.Users.Any(f => f.Id == userId)));
        return permissions.Any();
    }
}