namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class AssignmentBuilder
{
    private readonly Assignment _assignment;

    public AssignmentBuilder()
    {
        _assignment = new Assignment();
    }

    public AssignmentBuilder WithId(int id)
    {
        _assignment.Id = id;
        return this;
    }

    public AssignmentBuilder WithCaseId(int caseId)
    {
        _assignment.CaseId = caseId;
        return this;
    }

    public AssignmentBuilder WithTeamMemberId(int teamMemberId)
    {
        _assignment.TeamMemberId = teamMemberId;
        return this;
    }

    public AssignmentBuilder WithStatusId(int statusId)
    {
        _assignment.StatusId = statusId;
        return this;
    }

    public Assignment Build()
    {
        return _assignment;
    }
}