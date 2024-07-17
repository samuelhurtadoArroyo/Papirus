namespace Papirus.WebApi.Application.Dtos.DataExtractor;

[ExcludeFromCodeCoverage]
public class DocumentToProcessDto
{
    public FormatType FileType { get; set; }

    public string DocumentUrl { get; set; } = string.Empty;
}