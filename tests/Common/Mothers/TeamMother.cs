namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class TeamMother
{
    public static Team CreateTeam(int id, string name)
    {
        return new TeamBuilder()
               .WithId(id)
               .WithName(name)
               .Build();
    }

    public static Team DefaultTutelasTeam()
    {
        return CreateTeam(1, "Tutelas");
    }

    public static Team DefaultHipotecariosTeam()
    {
        return CreateTeam(2, "Hipotecarios");
    }

    public static Team DefaultCautelasTeam()
    {
        return CreateTeam(3, "Cautelas");
    }

    public static Team DefaultNotFoundTeam()
    {
        return CreateTeam(100, "NotFoundTeam");
    }

    public static List<Team> GetTeamList()
    {
        return [
            DefaultTutelasTeam(),
            DefaultHipotecariosTeam(),
            DefaultCautelasTeam()
        ];
    }

    public static List<Team> GetTeamList(int quantity)
    {
        var teamFaker = new Faker<Team>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.Name, f => $"TeamName_{f.IndexFaker}");

        return teamFaker.Generate(quantity);
    }

    public static List<Team> GetRandomTeamList(int quantity)
    {
        var teamFaker = new Faker<Team>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.Name, f => f.Name.Random.AlphaNumeric(50));

        return teamFaker.Generate(quantity);
    }
}