namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class FirmDtoBuilder
{
    private int _id;

    private string _name;

    private Guid _guidItentifier;

    public FirmDtoBuilder()
    {
        _id = 0;
        _name = string.Empty;
        _guidItentifier = Guid.NewGuid();
    }

    public FirmDtoBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public FirmDtoBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public FirmDtoBuilder WithGuidIdentifier(Guid guidIdentifier)
    {
        _guidItentifier = guidIdentifier;
        return this;
    }

    public FirmDto Build()
    {
        return new FirmDto
        {
            Id = _id,
            Name = _name,
            GuidIdentifier = _guidItentifier
        };
    }
}