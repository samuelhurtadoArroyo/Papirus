namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class AssignmentMother
{
    public static Assignment Create(int id, int caseId, int teamMemberId, int statusId)
    {
        return new Assignment
        {
            Id = id,
            CaseId = caseId,
            TeamMemberId = teamMemberId,
            StatusId = statusId
        };
    }

    public static Assignment DefaultAssignment()
    {
        return Create(1, 1, 1, 1);
    }

    public static Assignment AnotherAssignment()
    {
        return Create(2, 1, 2, 2);
    }

    public static List<Assignment> GetAssignmentList()
    {
        return
            [
                DefaultAssignment(),
                AnotherAssignment()
            ];
    }

    public static List<Assignment> GetAssignmentList(int quantity)
    {
        var assignmentList = new List<Assignment>
        {
                Create(0, 0, 0, 2)
            };

        var assignmentFaker = new Faker<Assignment>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.CaseId, f => f.Random.Int(1, 100))
            .RuleFor(o => o.TeamMemberId, f => f.Random.Int(1, 5))
            .RuleFor(o => o.StatusId, f => f.Random.Int(1, 3));

        assignmentList.AddRange(assignmentFaker.Generate(quantity));

        return assignmentList;
    }

    public static List<Assignment> GetAssignmentsForTeamMembers(List<int> teamMemberIds)
    {
        var assignments = new List<Assignment>();
        int id = 1;

        foreach (var teamMemberId in teamMemberIds)
        {
            assignments.Add(Create(id++, id, teamMemberId, 1));
        }

        return assignments;
    }

    public static List<Assignment> GetRandomAssignmnetList(int quantity)
    {
        var assignmentFaker = new Faker<Assignment>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.CaseId, f => f.Random.Int(1, 100))
            .RuleFor(o => o.TeamMemberId, f => f.Random.Int(1, 5))
            .RuleFor(o => o.StatusId, f => f.Random.Int(1, 3));

        return assignmentFaker.Generate(quantity);
    }

    public static Assignment CreateWithDetails(int id, int caseId, int teamMemberId, int statusId, TeamMember teamMember, string statusName)
    {
        return new Assignment
        {
            Id = id,
            CaseId = caseId,
            TeamMemberId = teamMemberId,
            StatusId = statusId,
            TeamMember = new TeamMember
            {
                TeamId = 1,
                MemberId = teamMember.Member.Id,
                IsLead = true,
                MaxCases = 5,
                AssignedCases = 0,
                Member = teamMember.Member,
                Team = teamMember.Team,
                Id = teamMember.Id
            },
            Status = new Status { Name = statusName },
            Case = CaseMother.DemandCase()
        };
    }
}