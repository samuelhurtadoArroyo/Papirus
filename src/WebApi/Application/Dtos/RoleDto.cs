namespace Papirus.WebApi.Application.Dtos;

/// <summary>
/// Represents a role with a unique identifier and name.
/// </summary>
public class RoleDto
{
    /// <summary>
    /// The unique identifier for the role.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the role.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}