namespace Papirus.WebApi.Application.Interfaces;

public interface IEmailAuthenticationService
{
    Task<string> AcquireTokenAsync();
}