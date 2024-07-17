namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class PermissionDtoBuilder
{
    private int _id;

    private string _name;

    private string _description;

    private string _labelCode;

    public PermissionDtoBuilder()
    {
        _id = 0;
        _name = string.Empty;
        _description = string.Empty;
        _labelCode = string.Empty;
    }

    public PermissionDtoBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public PermissionDtoBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public PermissionDtoBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public PermissionDtoBuilder WithLabelCode(string labelCode)
    {
        _labelCode = labelCode;
        return this;
    }

    public PermissionDto Build()
    {
        return new PermissionDto
        {
            Id = _id,
            Name = _name,
            Description = _description,
            PermissionLabelCode = _labelCode,
        };
    }
}