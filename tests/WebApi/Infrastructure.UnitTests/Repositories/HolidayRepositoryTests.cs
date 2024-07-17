namespace Papirus.WebApi.Infrastructure.Repositories.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class HolidayRepositoryTests
{
    private List<Holiday> _listHolidays = null!;

    private HolidayRepository _holidayRepository = null!;

    private Mock<AppDbContext> _mockAppDbContext = null!;

    [SetUp]
    public void Setup()
    {
        _listHolidays = HolidayMother.GetHolidaysList();
        _mockAppDbContext = new Mock<AppDbContext>();
        _mockAppDbContext.Setup(x => x.Set<Holiday>()).ReturnsDbSet(_listHolidays);
        _mockAppDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        _holidayRepository = new HolidayRepository(_mockAppDbContext.Object);
    }

    [Test]
    public async Task AddAsync_WhenCalled_AddsHolidaysuccessfully()
    {
        // Arrange
        Holiday holidayResponse = HolidayMother.DefaultAssign();
        var holldayRequest = holidayResponse;

        // Act
        var holidayResult = await _holidayRepository.AddAsync(holldayRequest);

        // Assert
        holidayResult.Should().NotBeNull();
        holidayResult.Should().BeEquivalentTo(holidayResponse);

        _mockAppDbContext.Verify(x => x.Set<Holiday>(), Times.Once);
        _mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task FindAsync_WhenCalled_ReturnsMatchingholiday()
    {
        // Arrange
        var holidayResponseExpected = HolidayMother.GetHolidaysList().Where(x => x.Id > 1);

        // Act
        var holidayResult = await _holidayRepository.FindAsync(x => x.Id > 1);

        // Asserts
        holidayResult.Should().NotBeNull();
        holidayResult.Should().BeEquivalentTo(holidayResponseExpected);
        _mockAppDbContext.Verify(x => x.Set<Holiday>(), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_WhenCalled_ReturnsAllHolidays()
    {
        // Arrange
        var HolidaysListExpected = HolidayMother.GetHolidaysList();

        // Act
        var holidayResult = await _holidayRepository.GetAllAsync();

        // Asserts
        holidayResult.Should().NotBeNull();
        holidayResult.Should().BeEquivalentTo(HolidaysListExpected);

        _mockAppDbContext.Verify(x => x.Set<Holiday>(), Times.Once);
    }
}