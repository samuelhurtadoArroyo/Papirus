namespace Papirus.WebApi.Application.Dtos.DataExtractor;

[ExcludeFromCodeCoverage]
public class DocumentFieldValueDto
{
    public int Id { get; set; }

    public int DocumentFieldId { get; set; }

    public string DocumentFieldName { get; set; } = string.Empty;

    public string FieldValue { get; set; } = string.Empty;
}