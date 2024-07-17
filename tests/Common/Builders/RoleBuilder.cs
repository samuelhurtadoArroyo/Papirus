namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class RoleBuilder
{
    private int _id;

    private string _name;

    public RoleBuilder()
    {
        _id = 0;
        _name = null!;
    }

    public RoleBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public RoleBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public Role Build()
    {
        return new Role
        {
            Id = _id,
            Name = _name
        };
    }
}