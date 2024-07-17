namespace Papirus.WebApi.Application.Dtos;

/// <summary>
/// Represents a team with a unique identifier and name.
/// </summary>
public class TeamDto
{
    /// <summary>
    /// The unique identifier for the team.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the team.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}