namespace Papirus.WebApi.Application.Dtos;

/// <summary>
/// Represents a case within the system
/// </summary>
[ExcludeFromCodeCoverage]
public class CaseWithAssignmentDto
{
    public CaseDto? Case { get; set; }

    public CaseAssignmentDto? Assignment { get; set; }
}