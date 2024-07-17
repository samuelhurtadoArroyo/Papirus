namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class TeamMemberMother
{
    public static TeamMember CreateTeamMember(int id, int teamId, int memberId, bool isLead, int maxCases)
    {
        return new TeamMemberBuilder()
               .WithId(id)
               .WithTeamId(teamId)
               .WithMemberId(memberId)
               .WithIsLead(isLead)
               .WithMaxCases(maxCases)
               .Build();
    }

    public static TeamMember CreateWithDetails(int id, int assignedCases, string firstName, string lastName)
    {
        return new TeamMember
        {
            Id = id,
            AssignedCases = assignedCases,
            Member = new User { Id = 1, FirstName = firstName, LastName = lastName }
        };
    }

    public static TeamMember DefaultTeamMemberLeader()
    {
        return CreateTeamMember(1, 1, 1, true, 5);
    }

    public static TeamMember DefaultTeamMember1()
    {
        return CreateTeamMember(2, 1, 2, false, 5);
    }

    public static TeamMember DefaultTeamMember2()
    {
        return CreateTeamMember(3, 1, 3, false, 5);
    }

    public static TeamMember DefaultTeamMember3()
    {
        return CreateTeamMember(4, 1, 4, false, 5);
    }

    public static TeamMember DefaultNotFoundTeamMember()
    {
        return CreateTeamMember(100, 0, 0, false, 0);
    }

    public static List<TeamMember> GetTeamMemberList()
    {
        return [
            DefaultTeamMemberLeader(),
            DefaultTeamMember1(),
            DefaultTeamMember2(),
            DefaultTeamMember3()
        ];
    }

    public static List<TeamMember> GetTeamMemberList(int quantity)
    {
        var teammemberFaker = new Faker<TeamMember>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.TeamId, f => 1)
            .RuleFor(o => o.MemberId, f => f.Random.Int(1, 1000))
            .RuleFor(o => o.IsLead, f => f.Random.Bool())
            .RuleFor(o => o.MaxCases, f => 5);

        return teammemberFaker.Generate(quantity);
    }

    public static List<TeamMember> GetRandomTeamMemberList(int quantity)
    {
        var teammemberFaker = new Faker<TeamMember>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.TeamId, f => 1)
            .RuleFor(o => o.MemberId, f => f.Random.Int(1, 1000))
            .RuleFor(o => o.IsLead, f => f.Random.Bool())
            .RuleFor(o => o.MaxCases, f => f.Random.Int(2, 5));

        return teammemberFaker.Generate(quantity);
    }
}