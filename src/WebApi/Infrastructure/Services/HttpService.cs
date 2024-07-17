using Papirus.WebApi.Application.Common.Models;
using Papirus.WebApi.Infrastructure.Common.Models;
using RestSharp;

namespace Papirus.WebApi.Infrastructure.Services;

[ExcludeFromCodeCoverage]
public class HttpService : IHttpService
{
    private readonly RestClient _client;

    private readonly TokenProvider _tokenProvider;

    private readonly ApiIdentifier _apiIdentifier;

    private readonly ILogger<HttpService> _logger;

    private readonly bool _useAuthentication;

    public HttpService(
        ApiCredentials settings,
        TokenProvider tokenProvider,
        ApiIdentifier apiIdentifier,
        ILogger<HttpService> logger,
        bool useAuthentication = true)
    {
        _tokenProvider = tokenProvider;
        _apiIdentifier = apiIdentifier;
        _client = new RestClient(settings.BaseUrl);
        _logger = logger;
        _useAuthentication = useAuthentication;
    }

    public async Task<TResponse> SendGetRequestAsync<TResponse>(string url) where TResponse : class, new()
    {
        var request = new RestRequest(url, Method.Get);
        try
        {
            if (_useAuthentication)
            {
                string token = await _tokenProvider.GetAccessTokenAsync(_apiIdentifier);
                request.AddHeader("Authorization", $"Bearer {token}");
            }

            _logger.LogInformation("Sending GET request to {Url}", url);
            var response = await _client.GetAsync<TResponse>(request);
            if (response == null)
            {
                _logger.LogError("Received null response for GET request to {Url}", url);
                throw new HttpRequestException("Received null response from the server.");
            }
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending GET request to {Url}", url);
            throw;
        }
    }

    public async Task<TResponse> SendPostRequestAsync<TRequest, TResponse>(string url, TRequest body, FileAttachment? fileAttachment = null, string? fileParameterName = null)
        where TRequest : class
    {
        var request = new RestRequest(url, Method.Post);
        try
        {
            if (_useAuthentication)
            {
                string token = await _tokenProvider.GetAccessTokenAsync(_apiIdentifier);
                request.AddHeader("Authorization", $"Bearer {token}");
            }
            if (fileAttachment != null && fileParameterName != null)
            {
                request.AlwaysMultipartFormData = true;
                request.AddFile(fileParameterName, fileAttachment.Content, fileAttachment.FileName, "multipart/form-data");

                _logger.LogInformation("Sending POST request to {Url} with file: {FileName}", url, fileAttachment.FileName);
                if (body is Dictionary<string, string> parameters)
                {
                    foreach (var param in parameters)
                    {
                        request.AddParameter(param.Key, param.Value);
                    }
                    _logger.LogInformation("Sending POST request to {Url} with body: {Body}", url, body);
                }
            }
            else
            {
                request.AddJsonBody(body);
                _logger.LogInformation("Sending POST request to {Url} with body: {Body}", url, body);
            }

            var response = await _client.PostAsync<TResponse>(request);
            if (response == null)
            {
                _logger.LogError("Received null response for POST request to {Url}", url);
                throw new HttpRequestException("Received null response from the server.");
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending POST request to {Url} with body: {Body}", url, body);
            throw;
        }
    }
}