namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class PermissionBuilder
{
    private int _id;

    private string _name;

    private string _description;

    private string _labelCode;

    public PermissionBuilder()
    {
        _id = 0;
        _name = null!;
        _description = null!;
        _labelCode = null!;
    }

    public PermissionBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public PermissionBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public PermissionBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public PermissionBuilder WithLabelCode(string labelCode)
    {
        _labelCode = labelCode;
        return this;
    }

    public Permission Build()
    {
        return new Permission
        {
            Id = _id,
            Name = _name,
            Description = _description,
            PermissionLabelCode = _labelCode,
        };
    }
}