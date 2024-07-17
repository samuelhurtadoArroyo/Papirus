namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class FirmDtoMother
{
    public static FirmDto Create(int id, string name, Guid guidIdentifier)
    {
        return new FirmDtoBuilder()
            .WithId(id)
            .WithName(name)
            .WithGuidIdentifier(guidIdentifier)
            .Build();
    }

    public static FirmDto GetGomezPinedaFirm()
    {
        return Create(1, "Gómez Pineda", Guid.Parse("811fb408-b41c-439b-9c20-125fc3783037"));
    }

    public static FirmDto GetAlianzaFirm()
    {
        return Create(2, "Alianza", Guid.Parse("60ffa5ac-b653-4b11-b68a-eb96300eb48d"));
    }

    public static FirmDto GetBgsfFirm()
    {
        return Create(3, "BGSF", Guid.Parse("514028b8-e03a-4d58-a045-8bf4ddb36b71"));
    }

    public static FirmDto GetDefaultFirm()
    {
        return GetGomezPinedaFirm();
    }

    public static FirmDto GetEmptyFirm()
    {
        return Create(0, null!, Guid.Empty);
    }

    public static FirmDto GetFirmWithMaxLengths()
    {
        return Create(1, "A".PadRight(ValidationConst.MaxFieldLength + 1, 'A'), Guid.NewGuid());
    }

    public static List<FirmDto> GetFirmList()
    {
        return [
            GetGomezPinedaFirm(),
            GetAlianzaFirm(),
            GetBgsfFirm(),
        ];
    }
}