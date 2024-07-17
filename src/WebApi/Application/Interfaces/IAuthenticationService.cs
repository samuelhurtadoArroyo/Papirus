namespace Papirus.WebApi.Application.Interfaces;

public interface IAuthenticationService
{
    Task<string> Login(string email, string password);

    Task<User> Register(User user, string password, int firmId);

    string GenerateJwtToken(User user);
}