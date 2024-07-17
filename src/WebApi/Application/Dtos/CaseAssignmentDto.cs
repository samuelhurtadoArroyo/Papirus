namespace Papirus.WebApi.Application.Dtos;

public class CaseAssignmentDto
{
    public int Id { get; set; }

    public int CaseId { get; set; }

    public int TeamMemberId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public int StatusId { get; set; }

    public string StatusName { get; set; } = string.Empty;
}