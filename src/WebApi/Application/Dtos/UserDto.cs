namespace Papirus.WebApi.Application.Dtos;

/// <summary>
/// Represents a user of the system.
/// </summary>
public class UserDto
{
    /// <summary>
    /// The unique identifier for the user.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The email address of the user.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The first name of the user.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// The last name of the user.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// The full name of the user.
    /// </summary>
    public string? FullName { get { return $"{FirstName} {LastName}"; } }

    /// <summary>
    /// The role of the user within the system.
    /// </summary>
    public int? RoleId { get; set; }

    /// <summary>
    /// Indicates whether the user is currently active.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// The firm that the user is associated with.
    /// </summary>
    public int? FirmId { get; set; }
}