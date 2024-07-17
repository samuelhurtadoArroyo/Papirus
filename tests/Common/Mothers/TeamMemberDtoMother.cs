namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class TeamMemberDtoMother
{
    public static TeamMemberDto CreateTeamMember(int id, int teamId, int memberId, bool isLead, int maxCases)
    {
        return new TeamMemberDtoBuilder()
            .WithId(id)
            .WithTeamId(teamId)
            .WithMemberId(memberId)
            .WithIsLead(isLead)
            .WithMaxCases(maxCases)
            .Build();
    }

    public static TeamMemberDto DefaultTeamMemberLeader()
    {
        return CreateTeamMember(1, 1, 1, true, 5);
    }

    public static TeamMemberDto DefaultTeamMember1()
    {
        return CreateTeamMember(2, 1, 2, false, 5);
    }

    public static TeamMemberDto DefaultTeamMember2()
    {
        return CreateTeamMember(3, 1, 3, false, 5);
    }

    public static TeamMemberDto DefaultTeamMember3()
    {
        return CreateTeamMember(4, 1, 4, false, 5);
    }

    public static TeamMemberDto GetEmptyTeamMember()
    {
        return CreateTeamMember(0, 0, 0, false, 0);
    }

    public static List<TeamMemberDto> GetTeamMemberList()
    {
        return [
            DefaultTeamMemberLeader(),
            DefaultTeamMember1(),
            DefaultTeamMember2(),
            DefaultTeamMember3()
        ];
    }

    public static List<TeamMemberDto> GetTeamMemberList(int quantity)
    {
        var teammemberFaker = new Faker<TeamMemberDto>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.TeamId, f => f.IndexFaker)
            .RuleFor(o => o.MemberId, f => f.IndexFaker)
            .RuleFor(o => o.IsLead, f => f.Random.Bool())
            .RuleFor(o => o.TeamId, f => f.IndexFaker);

        return teammemberFaker.Generate(quantity);
    }

    public static List<TeamMemberDto> GetRandomTeamMemberList(int quantity)
    {
        var teammemberFaker = new Faker<TeamMemberDto>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.TeamId, f => f.Random.Int())
            .RuleFor(o => o.MemberId, f => f.Random.Int())
            .RuleFor(o => o.IsLead, f => f.Random.Bool())
            .RuleFor(o => o.TeamId, f => f.Random.Int());

        return teammemberFaker.Generate(quantity);
    }
}