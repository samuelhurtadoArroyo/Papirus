namespace Papirus.WebApi.Application.Dtos;

/// <summary>
/// Represents the case document field value data transfer object.
/// </summary>
public class CaseDocumentFieldValueDto
{
    /// <summary>
    /// The unique identifier of the case document field value.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The identifier of the type document.
    /// </summary>
    public int DocumentTypeId { get; set; }

    /// <summary>
    /// The name of the document type.
    /// </summary>
    public string DocumentTypeName { get; set; } = string.Empty;

    /// <summary>
    /// The name of the case document field.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The label of the case document field.
    /// </summary>
    public string Tag { get; set; } = string.Empty;

    /// <summary>
    /// The value of the case document field.
    /// </summary>
    public string FieldValue { get; set; } = string.Empty;

    /// <summary>
    /// The identifier of the case.
    /// </summary>
    public int CaseId { get; set; }
}