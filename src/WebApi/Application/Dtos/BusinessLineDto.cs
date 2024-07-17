namespace Papirus.WebApi.Application.Dtos;

/// <summary>
/// Represents Business Line object.
/// </summary>
public class BusinessLineDto
{
    /// <summary>
    /// The unique identifier of the business line.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Business line name.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}