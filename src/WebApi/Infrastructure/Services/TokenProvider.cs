using Papirus.WebApi.Infrastructure.Common.Models;
using RestSharp;
using System.Collections.Concurrent;

namespace Papirus.WebApi.Infrastructure.Services;

[ExcludeFromCodeCoverage]
public class TokenProvider
{
    private readonly ConcurrentDictionary<ApiIdentifier, (string Token, DateTime Expiry)> _tokens = new();

    private readonly IDictionary<ApiIdentifier, ApiCredentials> _apiCredentials;

    public TokenProvider(
        IOptions<DataManagerApiOptions> dataManagerOptions,
        IOptions<DataExtractorApiOptions> dataExtractorOptions)
    {
        _apiCredentials = new Dictionary<ApiIdentifier, ApiCredentials>
        {
            { ApiIdentifier.DataManager, new ApiCredentials {
                BaseUrl = dataManagerOptions.Value.BaseUrl,
                TokenUrl = dataManagerOptions.Value.TokenUrl,
                ClientId = dataManagerOptions.Value.ClientId,
                ClientSecret = dataManagerOptions.Value.ClientSecret
            }},
            { ApiIdentifier.DataExtractor, new ApiCredentials {
                BaseUrl = dataExtractorOptions.Value.BaseUrl,
                TokenUrl = dataExtractorOptions.Value.TokenUrl,
                ClientId = dataExtractorOptions.Value.ClientId,
                ClientSecret = dataExtractorOptions.Value.ClientSecret
            }}
        };
    }

    public async Task<string> GetAccessTokenAsync(ApiIdentifier apiIdentifier)
    {
        if (_tokens.TryGetValue(apiIdentifier, out var tokenInfo) && DateTime.UtcNow < tokenInfo.Expiry)
        {
            return tokenInfo.Token;
        }

        var credentials = _apiCredentials[apiIdentifier];
        var client = new RestClient(credentials.TokenUrl);
        var request = new RestRequest() { Method = Method.Post };
        request.AddJsonBody(new
        {
            clientId = credentials.ClientId,
            clientSecret = credentials.ClientSecret,
            grant_type = "client_credentials"
        });

        var response = await client.ExecuteAsync<string>(request);
        if (response.Data != null && response.IsSuccessful)
        {
            var expiry = DateTime.UtcNow.AddHours(1);
            _tokens[apiIdentifier] = (response.Data, expiry);
            return response.Data;
        }
        else
        {
            throw new InvalidOperationException("Token retrieval failed: " + response.ErrorMessage);
        }
    }
}