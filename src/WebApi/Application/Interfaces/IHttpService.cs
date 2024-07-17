namespace Papirus.WebApi.Application.Interfaces;

public interface IHttpService
{
    Task<TResponse> SendGetRequestAsync<TResponse>(string url) where TResponse : class, new();

    Task<TResponse> SendPostRequestAsync<TRequest, TResponse>(string url, TRequest body, FileAttachment? fileAttachment = null, string? fileParameterName = null) where TRequest : class;
}