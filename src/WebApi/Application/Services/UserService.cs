namespace Papirus.WebApi.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [ExcludeFromCodeCoverage]
    [Obsolete("The Create method is deprecated. Please use the Register method in AuthenticationService instead.")]
    public async Task<User> Create(User model)
    {
        await Task.Yield();
        throw new NotImplementedException();
    }

    public async Task Delete(int id)
    {
        var original = await _userRepository.GetByIdAsync(id);

        if (original is not null)
        {
            await _userRepository.RemoveAsync(original);
            return;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<User> Edit(User model)
    {
        var id = model.Id;
        var original = await _userRepository.GetByIdAsync(id);

        if (original is not null)
        {
            return await _userRepository.UpdateAsync(model);
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User> GetById(int id)
    {
        var current = await _userRepository.GetByIdAsync(id);

        if (current is not null)
        {
            return current;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<QueryResult<User>> GetByQueryRequestAsync(QueryRequest queryRequest)
    {
        return await _userRepository.GetByQueryRequestAsync(queryRequest);
    }
}