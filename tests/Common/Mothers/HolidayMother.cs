namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class HolidayMother
{
    public static Holiday Create(int id, DateTime fecha, string descripcion)
    {
        return new Holiday()
        {
            Id = id,
            Date = fecha,
            Description = descripcion
        };
    }

    public static List<Holiday> GetHolidaysList()
    {
        return [Create(11, new(2024, 12, 6, 0, 0, 0, DateTimeKind.Utc), "DateTest"), Create(12, new(2024, 12, 6, 0, 0, 0, DateTimeKind.Utc), "DateTest_2")];
    }

    public static Holiday DefaultAssign()
    {
        return new Holiday()
        {
            Id = 12,
            Date = new(2024, 12, 6, 0, 0, 0, DateTimeKind.Utc),
            Description = "DefaultDescription"
        };
    }
}