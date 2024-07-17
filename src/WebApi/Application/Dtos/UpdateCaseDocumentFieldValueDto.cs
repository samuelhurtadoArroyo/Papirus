namespace Papirus.WebApi.Application.Dtos;

/// <summary>
/// Represents the case document field value to update.
/// </summary>
public class UpdateCaseDocumentFieldValueDto
{
    /// <summary>
    /// The unique identifier of the case document field value.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The value of the case document field.
    /// </summary>
    public string FieldValue { get; set; } = string.Empty;
}