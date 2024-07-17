namespace Papirus.WebApi.Application.Dtos;

/// <summary>
/// Represents a CaseProcessDocument within the system
/// </summary>
public class CaseProcessDocumentDto
{
    /// <summary>
    /// The unique identifier for the case.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The type id of the document
    /// </summary>
    public int DocumentTypeId { get; set; }

    /// <summary>
    /// The process document type id
    /// </summary>
    public int ProcessDocumentTypeId { get; set; }

    /// <summary>
    /// The id of the case for this document
    /// </summary>
    public int CaseId { get; set; }

    /// <summary>
    /// The name of the file
    /// </summary>
    public string FileName { get; set; } = null!;

    /// <summary>
    /// The path of the file
    /// </summary>
    public string FilePath { get; set; } = null!;
}