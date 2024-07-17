namespace Papirus.WebApi.Application.Interfaces;

public interface IDeadLineDateService
{
    public DateTime CalculateDeadlineAsync(DateTime submissionDate, double termDays);

    public Task<string> GetDeadlineStatusAsync(DateTime deadLineDate);

    public Task<List<DateTime>> GetHolidayDateAsync();
}