namespace Papirus.WebApi.Infrastructure.Common.Models;

[ExcludeFromCodeCoverage]
public class TokenResponse
{
    public string AccessToken { get; set; } = string.Empty;

    public int ExpiresIn { get; set; }
}