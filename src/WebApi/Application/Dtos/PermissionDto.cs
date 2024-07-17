namespace Papirus.WebApi.Application.Dtos;

/// <summary>
/// Represents a permission within the system.
/// </summary>
public class PermissionDto
{
    /// <summary>
    /// The unique identifier for the permission.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the permission.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// A brief description of what the permission allows.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Permission Label Code to identify
    /// </summary>
    public string PermissionLabelCode { get; set; } = string.Empty;
}