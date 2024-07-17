namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class ActorMother
{
    public static Actor Create(int id, int actorTypeId, int personId, int caseId)
    {
        return new ActorBuilder()
               .WithId(id)
               .WithActoryTypeId(actorTypeId)
               .WithPersonId(personId)
               .WithCaseId(caseId)
               .Build();
    }

    public static Actor ClaimantActor()
    {
        return Create(1, 1, 1, 1);
    }

    public static Actor DefenderActor()
    {
        return Create(2, 2, 2, 1);
    }

    public static List<Actor> GetActorList()
    {
        return [
            ClaimantActor(),
            DefenderActor()
        ];
    }

    public static List<Actor> GetActorList(int quantity)
    {
        var actorList = new List<Actor>
        {
            Create(0, 0, 0, 0)
        };

        var actorFaker = new Faker<Actor>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.ActorTypeId, _ => 1)
            .RuleFor(o => o.PersonId, _ => 1)
            .RuleFor(o => o.CaseId, _ => 1);

        actorList.AddRange(actorFaker.Generate(quantity));

        return actorList;
    }

    public static List<Actor> GetRandomActorList(int quantity)
    {
        var actorFaker = new Faker<Actor>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.ActorTypeId, f => f.Random.Int(1, 2))
            .RuleFor(o => o.PersonId, _ => 1)
            .RuleFor(o => o.CaseId, _ => 1);

        return actorFaker.Generate(quantity);
    }
}