namespace Papirus.WebApi.Application.Dtos;

/// <summary>
/// Represents a firm within the system.
/// </summary>
public class FirmDto
{
    /// <summary>
    /// The unique identifier for the firm.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the firm.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// A unique GUID identifier generated for the firm. Defaults to a new GUID upon instantiation.
    /// </summary>
    public Guid GuidIdentifier { get; set; } = Guid.NewGuid();
}