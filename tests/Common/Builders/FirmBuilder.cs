namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class FirmBuilder
{
    private int _id;

    private string _name;

    private Guid _guidIdentifier;

    public FirmBuilder()
    {
        _id = 0;
        _guidIdentifier = Guid.NewGuid();
        _name = null!;
    }

    public FirmBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public FirmBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public FirmBuilder WithGuidIdentifier(Guid guidIdentifier)
    {
        _guidIdentifier = guidIdentifier;
        return this;
    }

    public Firm Build()
    {
        return new Firm
        {
            Id = _id,
            Name = _name,
            GuidIdentifier = _guidIdentifier
        };
    }
}