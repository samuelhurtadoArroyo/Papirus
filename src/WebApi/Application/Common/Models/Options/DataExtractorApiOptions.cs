namespace Papirus.WebApi.Application.Common.Models.Options;

[ExcludeFromCodeCoverage]
public class DataExtractorApiOptions
{
    public string BaseUrl { get; set; } = string.Empty;

    public string TokenUrl { get; set; } = string.Empty;

    public string ClientId { get; set; } = string.Empty;

    public string ClientSecret { get; set; } = string.Empty;
}