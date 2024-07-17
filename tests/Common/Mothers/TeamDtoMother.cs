namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class TeamDtoMother
{
    public static TeamDto CreateTeam(int id, string name)
    {
        return new TeamDtoBuilder()
               .WithId(id)
               .WithName(name)
               .Build();
    }

    public static TeamDto DefaultTutelasTeam()
    {
        return CreateTeam(1, "Tutelas");
    }

    public static TeamDto DefaultHipotecariosTeam()
    {
        return CreateTeam(2, "Hipotecarios");
    }

    public static TeamDto DefaultCautelasTeam()
    {
        return CreateTeam(3, "Cautelas");
    }

    public static TeamDto DefaultNotFoundTeam()
    {
        return CreateTeam(100, "NotFoundTeamDto");
    }

    public static TeamDto GetEmptyTeam()
    {
        return CreateTeam(0, null!);
    }

    public static TeamDto GetTeamWithMaxLengths()
    {
        return CreateTeam(1, "A".PadRight(ValidationConst.MaxFieldLength + 1, 'A'));
    }

    public static List<TeamDto> GetTeamList()
    {
        return [
            DefaultTutelasTeam(),
            DefaultHipotecariosTeam(),
            DefaultCautelasTeam()
        ];
    }

    public static List<TeamDto> GetTeamList(int quantity)
    {
        var teamFaker = new Faker<TeamDto>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.Name, f => $"TeamName{f.IndexFaker}");

        return teamFaker.Generate(quantity);
    }

    public static List<TeamDto> GetRandomTeamList(int quantity)
    {
        var teamFaker = new Faker<TeamDto>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.Name, f => f.Name.Random.AlphaNumeric(50));

        return teamFaker.Generate(quantity);
    }
}