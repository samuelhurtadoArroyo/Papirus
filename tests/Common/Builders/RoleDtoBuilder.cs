namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class RoleDtoBuilder
{
    private int _id;

    private string _name;

    public RoleDtoBuilder()
    {
        _id = 0;
        _name = string.Empty;
    }

    public RoleDtoBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public RoleDtoBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public RoleDto Build()
    {
        return new RoleDto
        {
            Id = _id,
            Name = _name
        };
    }
}