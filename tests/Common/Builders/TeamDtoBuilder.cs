namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class TeamDtoBuilder
{
    private int _id;

    private string _name;

    public TeamDtoBuilder()
    {
        _id = 0;
        _name = string.Empty;
    }

    public TeamDtoBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public TeamDtoBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public TeamDto Build()
    {
        return new TeamDto
        {
            Id = _id,
            Name = _name
        };
    }
}