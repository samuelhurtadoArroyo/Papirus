namespace Papirus.WebApi.Application.Dtos;

/// <summary>
/// Represents a user login response.
/// </summary>
public class LoginDto
{
    /// <summary>
    /// The authentication token provided upon successful login.
    /// </summary>
    public string Token { get; set; } = string.Empty;
}