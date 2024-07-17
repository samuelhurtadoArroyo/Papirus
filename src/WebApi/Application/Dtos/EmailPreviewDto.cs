namespace Papirus.WebApi.Application.Dtos;

/// <summary>
/// Represents a case within the system
/// </summary>
public class EmailPreviewDto
{
    /// <summary>
    /// Case dto.
    /// </summary>
    public CaseDto? Case { get; set; }

    /// <summary>
    /// Case documents
    /// </summary>
    public IEnumerable<CaseDocumentFieldValueDto>? CaseDocuments { get; set; }
}