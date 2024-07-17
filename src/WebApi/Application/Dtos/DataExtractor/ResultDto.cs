namespace Papirus.WebApi.Application.Dtos.DataExtractor;

[ExcludeFromCodeCoverage]
public class ResultDto
{
    public DocumentTypeDto DocumentType { get; set; } = new();

    public string DocumentContent { get; set; } = string.Empty;

    public List<DocumentFieldValueDto>? DocumentFields { get; set; }
}