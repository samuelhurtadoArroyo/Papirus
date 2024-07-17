namespace Papirus.WebApi.Application.Dtos;

/// <summary>
/// Represents the user's login credentials.
/// </summary>
public class LoginInputDto
{
    /// <summary>
    /// The user's email address used for logging in.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The user's password used for logging in.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}