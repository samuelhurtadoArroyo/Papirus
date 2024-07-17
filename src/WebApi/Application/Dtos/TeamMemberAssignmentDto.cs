namespace Papirus.WebApi.Application.Dtos;

public class TeamMemberAssignmentDto
{
    public int MemberId { get; set; }

    public string FullName { get; set; } = null!;

    public string CaseLoad { get; set; } = null!;
}