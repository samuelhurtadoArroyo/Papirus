namespace Papirus.WebApi.Application.Services.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class DeadLineDateServiceTests
{
    private DeadLineDateService _mockDeadLineDateService = null!;

    private List<DateTime> _holidays = null!;

    private Mock<IHolidayRepository> _mockHolidayRepository = null!;

    [SetUp]
    public void SetUp()
    {
        _mockHolidayRepository = new Mock<IHolidayRepository>();

        _holidays =
        [
            DateMother.Create(2023, 12, 25),
            DateMother.Create(2024, 6, 17)
        ];
        _mockDeadLineDateService = new DeadLineDateService(_mockHolidayRepository.Object);
    }

    [Test]
    public void CalculateDeadline_WhenNotificationOnBusinessDay_ReturnsCorrectDeadline()
    {
        DateTime submissionDate = DateMother.Create(2024, 6, 10, 9);
        const int termHours = 72;
        DateTime expectedDeadline = DateMother.Create(2024, 6, 13, 9);

        var result = _mockDeadLineDateService.CalculateDeadlineAsync(submissionDate, termHours);

        // Asserts
        result.Should().Be(expectedDeadline);
    }

    [Test]
    public void CalculateDeadline_WhenNotificationOnWeekend_ShiftsToNextBusinessDay()
    {
        DateTime submissionDate = DateMother.Create(2024, 6, 15, 9); // Sábado
        const double termHours = 72;
        DateTime expectedDeadline = DateMother.Create(2024, 6, 20, 7);

        var result = _mockDeadLineDateService.CalculateDeadlineAsync(submissionDate, termHours);

        // Asserts
        result.Should().Be(expectedDeadline);
    }

    [Test]
    public async Task GetHolidayDate_WhenHolidaysIsEmpty_ReturnsListOfHolidaysAsync()
    {
        // Arrange
        List<Holiday> holidays = [];

        // Act
        _mockHolidayRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(holidays);

        var result = await _mockDeadLineDateService.GetHolidayDateAsync();

        // Asserts
        result.Should().BeAssignableTo<IEnumerable<DateTime>>();
    }

    [Test]
    public async Task GetHolidayDate_WhenHolidaysIsNotEmpty_ReturnsListOfHolidaysAsync()
    {
        // Arrange
        List<Holiday> holidays = [];
        holidays.Add(new Holiday { Date = DateMother.Create(2023, 12, 25), Description = "", Id = 1 });
        holidays.Add(new Holiday { Date = DateMother.Create(2024, 6, 17), Description = "", Id = 1 });

        // Act
        _mockHolidayRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(holidays);

        var result = await _mockDeadLineDateService.GetHolidayDateAsync();

        // Asserts
        result.Should().BeAssignableTo<IEnumerable<DateTime>>();
        result.Should().Equal(_holidays);
    }
}