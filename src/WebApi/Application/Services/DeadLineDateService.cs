using Papirus.WebApi.Domain.Define.Enums;
using Papirus.WebApi.Domain.Entities;

namespace Papirus.WebApi.Application.Services;

public class DeadLineDateService : IDeadLineDateService
{
    private readonly IHolidayRepository _holidayRepository;

    private readonly List<DateTime> _holidays;
    private static readonly TimeSpan WorkDayStart = new(7, 0, 0);
    private static readonly TimeSpan WorkDayEnd = new(17, 0, 0);

    public DeadLineDateService(IHolidayRepository holidayRepository)
    {
        _holidayRepository = holidayRepository;
        _holidays = GetHolidayDateAsync().Result;
    }

    public DateTime CalculateDeadlineAsync(DateTime startDate, double hoursToAdd)
    {
        DateTime currentDate = AdjustToWorkDay(startDate);

        if (hoursToAdd < 24)
        {
            return CalculateWithinDayDeadline(currentDate, hoursToAdd);
        }
        else
        {
            return CalculateMultiDayDeadline(currentDate, hoursToAdd);
        }
    }

    [ExcludeFromCodeCoverage]
    public async Task<string> GetDeadlineStatusAsync(DateTime deadLineDate)
    {
        DateTime currentDate = DateTime.UtcNow;
        return await Task.FromResult(GetColorNotification((deadLineDate - currentDate).Hours));
    }

    public async Task<List<DateTime>> GetHolidayDateAsync()
    {
        List<DateTime> holidaysList = [];
        var repository = await _holidayRepository.GetAllAsync();
        foreach (var date in repository)
        {
            holidaysList.Add(date.Date);
        }
        return holidaysList;
    }

    [ExcludeFromCodeCoverage]
    private static string GetColorNotification(int deadLineHours)
    {
        if (deadLineHours <= 0)
        {
            deadLineHours = 1;
        }
        else if (deadLineHours is > 0 and <= 10)
        {
            deadLineHours = 2;
        }
        else if (deadLineHours is > 10 and <= 20)
        {
            deadLineHours = 3;
        }
        else if (deadLineHours > 20)
        {
            deadLineHours = 4;
        }
        NotificationColors color = (NotificationColors)deadLineHours;
        return color.ToString();
    }

    private DateTime CalculateWithinDayDeadline(DateTime currentDate, double hoursToAdd)
    {
        while (hoursToAdd > 0)
        {
            if (IsWorkDay(currentDate, _holidays))
            {
                TimeSpan workTimeRemainingToday = GetWorkTimeRemainingToday(currentDate);

                if (hoursToAdd <= workTimeRemainingToday.TotalHours)
                {
                    currentDate = currentDate.AddHours(hoursToAdd);
                    break;
                }
                else
                {
                    currentDate = currentDate.AddHours(workTimeRemainingToday.TotalHours);
                    hoursToAdd -= workTimeRemainingToday.TotalHours;
                }
            }

            currentDate = GetNextWorkDay(currentDate, _holidays);
            currentDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, WorkDayStart.Hours, WorkDayStart.Minutes, WorkDayStart.Seconds);
        }

        return currentDate;
    }

    private DateTime CalculateMultiDayDeadline(DateTime currentDate, double hoursToAdd)
    {
        double daysToAdd = Math.Floor(hoursToAdd / 24);

        while (!IsWorkDay(currentDate, _holidays))
        {
            currentDate = GetNextWorkDay(currentDate, _holidays);
            currentDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, WorkDayStart.Hours, WorkDayStart.Minutes, WorkDayStart.Seconds);
        }

        while (daysToAdd > 0)
        {
            currentDate = currentDate.AddDays(1);
            while (!IsWorkDay(currentDate, _holidays))
            {
                currentDate = currentDate.AddDays(1);
            }
            daysToAdd--;
        }

        return currentDate;
    }

    private DateTime AdjustToWorkDay(DateTime currentDate)
    {
        if (currentDate.TimeOfDay < WorkDayStart)
        {
            currentDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, WorkDayStart.Hours, WorkDayStart.Minutes, WorkDayStart.Seconds);
        }

        if (currentDate.TimeOfDay > WorkDayEnd)
        {
            currentDate = GetNextWorkDay(currentDate, _holidays);
            currentDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, WorkDayStart.Hours, WorkDayStart.Minutes, WorkDayStart.Seconds);
        }

        return currentDate;
    }

    private static bool IsWorkDay(DateTime date, List<DateTime> holidays)
    {
        return !holidays.Contains(date.Date) && date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
    }

    private static TimeSpan GetWorkTimeRemainingToday(DateTime date)
    {
        if (date.TimeOfDay < WorkDayStart)
        {
            return WorkDayEnd - WorkDayStart;
        }
        else if (date.TimeOfDay > WorkDayEnd)
        {
            return TimeSpan.Zero;
        }
        else
        {
            return WorkDayEnd - date.TimeOfDay;
        }
    }

    private static DateTime GetNextWorkDay(DateTime date, List<DateTime> holidays)
    {
        DateTime nextDay = date.Date.AddDays(1);
        while (!IsWorkDay(nextDay, holidays))
        {
            nextDay = nextDay.AddDays(1);
        }
        return nextDay;
    }
}