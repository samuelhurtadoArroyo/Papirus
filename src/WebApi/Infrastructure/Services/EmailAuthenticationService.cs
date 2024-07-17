using Microsoft.Identity.Client;

namespace Papirus.WebApi.Infrastructure.Services;

[ExcludeFromCodeCoverage]
public class EmailAuthenticationService : IEmailAuthenticationService
{
    private readonly EmailServiceAdOptions _emailServiceAdOptions;

    public EmailAuthenticationService(IOptions<EmailServiceAdOptions> azureAdOption)
    {
        _emailServiceAdOptions = azureAdOption.Value;
    }

    public async Task<string> AcquireTokenAsync()
    {
        var confidentialClientApplication = ConfidentialClientApplicationBuilder
            .Create(_emailServiceAdOptions.ClientId)
            .WithAuthority($"{_emailServiceAdOptions.Authority}/{_emailServiceAdOptions.TenantId}/")
            .WithClientSecret(_emailServiceAdOptions.ClientSecret)
            .Build();

        var authTokenResult = await confidentialClientApplication
            .AcquireTokenForClient(_emailServiceAdOptions.Scope)
            .ExecuteAsync();

        return authTokenResult.AccessToken;
    }
}