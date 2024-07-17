namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class DateMother
{
    public static DateTime Create(int year, int month, int day, int hour = 0, DateTimeKind kind = DateTimeKind.Utc)
    {
        return new DateTime(year, month, day, hour, 0, 0, kind);
    }

    public static List<DateTime> GetList(int quantity, int minYear)
    {
        List<DateTime> list = [];

        Random rnd = new();
        DateTime datetoday = DateTime.Now;

        for (int i = 0; i < quantity; i++)
        {
            int rndYear = rnd.Next(minYear, datetoday.Year);
            int rndMonth = rnd.Next(1, 12);
            int rndDay = rnd.Next(1, 31);

            list.Add(Create(rndYear, rndMonth, rndDay));
        }

        return list;
    }
}