namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class FirmMother
{
    public static Firm Create(int id, string name, Guid guidIdentifier)
    {
        return new FirmBuilder()
            .WithId(id)
            .WithName(name)
            .WithGuidIdentifier(guidIdentifier)
            .Build();
    }

    public static Firm DefaultFirm()
    {
        return Create(1, "Gómez Pineda", Guid.Parse("811fb408-b41c-439b-9c20-125fc3783037"));
    }

    public static List<Firm> GetFirmList()
    {
        return
        [
            DefaultFirm(),
            Create(2, "Alianza", Guid.Parse("60ffa5ac-b653-4b11-b68a-eb96300eb48d")),
            Create(3, "BGSF", Guid.Parse("514028b8-e03a-4d58-a045-8bf4ddb36b71"))
        ];
    }

    public static List<Firm> GetFirmList(int quantity)
    {
        var firmList = new List<Firm>()
        {
            Create(0, "", Guid.Empty)
        };

        var firmFaker = new Faker<Firm>()
            .RuleFor(o => o.Id, f => f.IndexFaker + 1)
            .RuleFor(o => o.Name, f => $"FirmName{f.IndexFaker + 1}");

        firmList.AddRange(firmFaker.Generate(quantity));

        return firmList;
    }

    public static List<Firm> GetRandomFirmList(int quantity)
    {
        var roleFaker = new Faker<Firm>()
            .RuleFor(o => o.Id, f => f.IndexFaker + 1)
            .RuleFor(o => o.Name, f => f.Name.Random.AlphaNumeric(50));

        return roleFaker.Generate(quantity);
    }
}