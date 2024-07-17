using Papirus.WebApi.Application.Dtos.DataExtractor;

namespace Papirus.WebApi.Application.Interfaces;

public interface IDataExtractorService
{
    Task<ResultDto> ProcessDocumentAsync(DocumentToProcessDto documentToProcess);
}