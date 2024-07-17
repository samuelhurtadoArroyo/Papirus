namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class TeamBuilder
{
    private int _id;

    private string _name;

    public TeamBuilder()
    {
        _id = 0;
        _name = null!;
    }

    public TeamBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public TeamBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public Team Build()
    {
        return new Team
        {
            Id = _id,
            Name = _name
        };
    }
}