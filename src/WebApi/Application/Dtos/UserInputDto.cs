namespace Papirus.WebApi.Application.Dtos;

public class UserInputDto
{
    /// <summary>
    /// The unique identifier for the user.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The first name of the user.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// The last name of the user.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// The email address of the user.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The password for the user's account with at least one special character,
    /// one upper case letter, one lower case letter, one number and at least 8 characters.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// The role of the user within the system.
    /// </summary>
    public int? RoleId { get; set; }
}