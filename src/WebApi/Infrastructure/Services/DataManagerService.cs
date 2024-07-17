using Papirus.WebApi.Application.Common.Models;

namespace Papirus.WebApi.Infrastructure.Services;

[ExcludeFromCodeCoverage]
public class DataManagerService : IDataManagerService
{
    private readonly IHttpService _httpService;

    private readonly ILogger<DataManagerService> _logger;

    public DataManagerService(IHttpService httpService, ILogger<DataManagerService> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }

    public async Task<string> UploadFileAsync(FileAttachment fileAttachment, string containerRoute)
    {
        try
        {
            _logger.LogInformation("Starting uploading process for file: {FileName}", fileAttachment.FileName);

            var result = await _httpService.SendPostRequestAsync<byte[], string>($"/DataManager/UploadFile?filePathName={containerRoute}", fileAttachment.Content!, fileAttachment, "file");

            _logger.LogInformation("Upload process completed successfully for file: {FileName}", fileAttachment.FileName);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading file: {FileName}", fileAttachment.FileName);
            throw;
        }
    }
}