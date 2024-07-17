namespace Papirus.WebApi.Application.Interfaces;

public interface ICurrentUserService
{
    Task<int> GetCurrentUserIdAsync();
}