﻿namespace Papirus.WebApi.Infrastructure.Common.Models;

[ExcludeFromCodeCoverage]
public class ApiCredentials
{
    public string BaseUrl { get; set; } = string.Empty;

    public string TokenUrl { get; set; } = string.Empty;

    public string ClientId { get; set; } = string.Empty;

    public string ClientSecret { get; set; } = string.Empty;
}