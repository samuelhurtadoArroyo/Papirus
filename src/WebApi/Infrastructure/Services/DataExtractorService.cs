using Papirus.WebApi.Application.Dtos.DataExtractor;

namespace Papirus.WebApi.Infrastructure.Services;

[ExcludeFromCodeCoverage]
public class DataExtractorService : IDataExtractorService
{
    private readonly IHttpService _httpService;

    private readonly ILogger<DataExtractorService> _logger;

    public DataExtractorService(IHttpService httpService, ILogger<DataExtractorService> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }

    public async Task<ResultDto> ProcessDocumentAsync(DocumentToProcessDto documentToProcess)
    {
        try
        {
            _logger.LogInformation("Starting document processing for URL: {DocumentUrl}", documentToProcess.DocumentUrl);

            var result = await _httpService.SendPostRequestAsync<DocumentToProcessDto, ResultDto>("DataExtract/ProcessDocument", documentToProcess);

            _logger.LogInformation("Document processing completed successfully for URL: {DocumentUrl}", documentToProcess.DocumentUrl);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing document for URL: {DocumentUrl}", documentToProcess.DocumentUrl);
            throw;
        }
    }
}