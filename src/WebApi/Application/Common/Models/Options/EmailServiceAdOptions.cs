namespace Papirus.WebApi.Application.Common.Models.Options;

[ExcludeFromCodeCoverage]
public class EmailServiceAdOptions
{
    public string ClientId { get; set; } = string.Empty;

    public string TenantId { get; set; } = string.Empty;

    public string ClientSecret { get; set; } = string.Empty;

    public string Authority { get; set; } = string.Empty;

    public string[] Scope { get; set; } = [];
}