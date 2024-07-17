using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.WebApi.Infrastructure.Repositories.Tests;

[ExcludeFromCodeCoverage]
public class FirmRepositoryTests
{
    private List<Firm> firmList = null!;

    private FirmRepository firmRepository = null!;

    private Mock<AppDbContext> mockAppDbContext = null!;

    [SetUp]
    public void SetUp()
    {
        firmList = FirmMother.GetFirmList();
        mockAppDbContext = new Mock<AppDbContext>();
        mockAppDbContext.Setup(x => x.Set<Firm>()).ReturnsDbSet(firmList);
        mockAppDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        firmRepository = new FirmRepository(mockAppDbContext.Object);
    }

    [Test]
    public async Task AddAsync_WhenCalled_AddsFirmSuccessfully()
    {
        // Arrange
        Firm firmResponseExpected = FirmMother.DefaultFirm();
        var firmRequest = firmResponseExpected;

        // Act
        var firmResult = await firmRepository.AddAsync(firmRequest);

        // Assert
        firmResult.Should().NotBeNull();
        firmResult.Should().BeEquivalentTo(firmResponseExpected);

        mockAppDbContext.Verify(x => x.Set<Firm>(), Times.Once);
        mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task FindAsync_WhenCalled_ReturnsMatchingFirms()
    {
        // Arrange
        var firmListResponseExpected = FirmMother.GetFirmList().Where(x => x.Id > 1);

        // Act
        var firmListResult = await firmRepository.FindAsync(x => x.Id > 1);

        // Asserts
        firmListResult.Should().NotBeNull();
        firmListResult.Should().BeEquivalentTo(firmListResponseExpected);

        mockAppDbContext.Verify(x => x.Set<Firm>(), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_WhenCalled_ReturnsAllFirms()
    {
        // Arrange
        var firmListResponseExpected = FirmMother.GetFirmList();

        // Act
        var firmListResult = await firmRepository.GetAllAsync();

        // Asserts
        firmListResult.Should().NotBeNull();
        firmListResult.Should().BeEquivalentTo(firmListResponseExpected);

        mockAppDbContext.Verify(x => x.Set<Firm>(), Times.Once);
    }

    [Test]
    public async Task GetByIdAsync_WhenCalled_ReturnsExpectedFirm()
    {
        // Arrange
        var firmResponseExpected = FirmMother.DefaultFirm();
        int id = firmResponseExpected.Id;

        // Act
        var firmResult = await firmRepository.GetByIdAsync(id);

        // Assert
        firmResult.Should().NotBeNull();
        firmResult.Should().BeEquivalentTo(firmResponseExpected);

        mockAppDbContext.Verify(x => x.Set<Firm>(), Times.Once);
    }

    [Test]
    public async Task RemoveAsync_WhenCalled_RemovesFirmSuccessfully()
    {
        // Arrange
        var firmResponseExpected = FirmMother.DefaultFirm();
        var firmRequest = firmResponseExpected;

        // Act
        await firmRepository.RemoveAsync(firmRequest);

        // Assert
        mockAppDbContext.Verify(x => x.Set<Firm>().Remove(It.IsAny<Firm>()), Times.Once);
        mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_WhenCalled_UpdatesFirmSuccessfully()
    {
        // Arrange
        var firmResponseExpected = FirmMother.DefaultFirm();
        var firmRequest = firmResponseExpected;

        // Act
        var firmResult = await firmRepository.UpdateAsync(firmRequest);

        // Assert
        firmResult.Should().NotBeNull();
        firmResult.Should().BeEquivalentTo(firmResponseExpected);

        mockAppDbContext.Verify(x => x.Set<Firm>().Update(It.IsAny<Firm>()), Times.Once);
        mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Ignore("Due date")]
    [TestCase(null, null, null, null, null, null, null)]
    public async Task GetByQueryRequestAsync_WhenCalledWithParameters_ReturnsExpectedFirms(int? pageNumber, int? pageSize, string? searchString, string? columnName, FilterOptions? filterOptions, string? filterValue, SortOrders? sortOrders)
    {
        // Arrange
        var firmListResponseExpected = FirmMother.GetFirmList(CommonConst.MaxCount);
        var filterParams = FilterParamsMother.GetFilterParams(columnName!, filterOptions ?? FilterOptions.IsNotEmpty, filterValue!);
        var sortingParams = SortingParamsMother.GetSortingParams(columnName!, sortOrders ?? SortOrders.Asc);
        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, searchString, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<Firm>.Create(firmListResponseExpected, queryRequest);

        mockAppDbContext.Setup(x => x.Set<Firm>()).ReturnsDbSet(firmListResponseExpected);

        // Act
        var queryResult = await firmRepository.GetByQueryRequestAsync(queryRequest);

        // Asserts
        queryResult.Should().NotBeNull();
        queryResult.Items.Should().NotBeNull();
        queryResult.PaginationData.Should().NotBeNull();
        queryResult.Items.Should().BeEquivalentTo(queryResultExpected.Items);
        queryResult.PaginationData.Should().BeEquivalentTo(queryResultExpected.PaginationData);

        mockAppDbContext.Verify(x => x.Set<Firm>(), Times.Once);
    }
}