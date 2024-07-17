using Microsoft.AspNetCore.Http;

namespace Papirus.WebApi.Application.Services;

[ExcludeFromCodeCoverage]
public class CurrentUserService : ICurrentUserService
{
    private readonly HttpContext _httpContext;

    private readonly IUserRepository _userRepository;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    {
        _httpContext = httpContextAccessor.HttpContext;
        _userRepository = userRepository;
    }

    [ExcludeFromCodeCoverage]
    public async Task<int> GetCurrentUserIdAsync()
    {
        if (_httpContext is null)
            return -1;

        string userName = _httpContext.User.Identity?.Name ?? string.Empty;
        var user = await _userRepository.FindByEmailAsync(userName);
        var userId = -1;
        if (user is not null)
            userId = user.Id;

        return userId;
    }
}