namespace Papirus.WebApi.Application.Dtos;

/// <summary>
/// Represents the association of a member with a team, including their role (Lead) and capacity.
/// </summary>
public class TeamMemberDto
{
    /// <summary>
    /// The unique identifier in TeamMembers table.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Team's unique identifier.
    /// </summary>
    public int TeamId { get; set; }

    /// <summary>
    /// Member's unique identifier.
    /// </summary>
    public int MemberId { get; set; }

    /// <summary>
    /// Indicates whether the member is the lead of the team.
    /// </summary>
    public bool IsLead { get; set; } = false;

    /// <summary>
    /// The maximum number of cases the member can handle.
    /// </summary>
    public int MaxCases { get; set; }

    /// <summary>
    /// The number of cases assigned to the member.
    /// </summary>
    public int AssignedCases { get; set; }

    /// <summary>
    /// Member's user data.
    /// </summary>
    public UserDto? Member { get; set; }
}