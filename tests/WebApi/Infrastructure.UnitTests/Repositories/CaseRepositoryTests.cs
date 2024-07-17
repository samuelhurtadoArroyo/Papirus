using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.WebApi.Infrastructure.Repositories.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class CaseRepositoryTests
{
    private List<Case> caseList = null!;

    private CaseRepository caseRepository = null!;

    private Mock<AppDbContext> mockAppDbContext = null!;

    private static EquivalencyAssertionOptions<Case> ExcludeProperties(EquivalencyAssertionOptions<Case> options)
    {
        return options;
    }

    [SetUp]
    public void SetUp()
    {
        caseList = CaseMother.GetCaseList();
        mockAppDbContext = new Mock<AppDbContext>();
        mockAppDbContext.Setup(x => x.Set<Case>()).ReturnsDbSet(caseList);
        mockAppDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        caseRepository = new CaseRepository(mockAppDbContext.Object);
    }

    [Test]
    public async Task AddAsync_WhenCalled_AddsCaseSuccessfully()
    {
        // Arrange
        Case caseResponseExpected = CaseMother.DemandCase();
        var caseRequest = caseResponseExpected;

        // Act
        var userResult = await caseRepository.AddAsync(caseRequest);

        // Assert
        userResult.Should().NotBeNull();
        userResult.Should().BeEquivalentTo(caseResponseExpected);

        mockAppDbContext.Verify(x => x.Set<Case>(), Times.Once);
        mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task FindAsync_WhenCalled_ReturnsMatchingCases()
    {
        // Arrange
        var caseListResponseExpected = CaseMother.GetCaseList().Where(x => x.Id > 1);

        // Act
        var caseListResult = await caseRepository.FindAsync(x => x.Id > 1);

        // Asserts
        caseListResult.Should().NotBeNull();
        caseListResult.Should().BeEquivalentTo(caseListResponseExpected, ExcludeProperties);

        mockAppDbContext.Verify(x => x.Set<Case>(), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_WhenCalled_ReturnsAllCases()
    {
        // Arrange
        var caseListResponseExpected = CaseMother.GetCaseList();

        // Act
        var caseListResult = await caseRepository.GetAllAsync();

        // Asserts
        caseListResult.Should().NotBeNull();
        caseListResult.Should().BeEquivalentTo(caseListResponseExpected, ExcludeProperties);

        mockAppDbContext.Verify(x => x.Set<Case>(), Times.Once);
    }

    [Test]
    public async Task GetByIdAsync_WhenCalled_ReturnsExpectedCase()
    {
        // Arrange
        var caseResponseExpected = CaseMother.DemandCase();
        int id = caseResponseExpected.Id;

        // Act
        var caseResult = await caseRepository.GetByIdAsync(id);

        // Assert
        caseResult.Should().NotBeNull();
        caseResult.Should().BeEquivalentTo(caseResponseExpected, ExcludeProperties);

        mockAppDbContext.Verify(x => x.Set<Case>(), Times.Once);
    }

    [Test]
    public async Task RemoveAsync_WhenCalled_RemovesCaseSuccessfully()
    {
        // Arrange
        var caseRequest = CaseMother.DemandCase();

        // Act
        await caseRepository.RemoveAsync(caseRequest);

        // Assert
        mockAppDbContext.Verify(x => x.Set<Case>().Remove(It.IsAny<Case>()), Times.Once);
        mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_WhenCalled_UpdatesCaseSuccessfully()
    {
        // Arrange
        var caseResponseExpected = CaseMother.DemandCase();
        var caseRequest = caseResponseExpected;

        // Act
        var personResult = await caseRepository.UpdateAsync(caseRequest);

        // Assert
        personResult.Should().NotBeNull();
        personResult.Should().BeEquivalentTo(caseResponseExpected);

        mockAppDbContext.Verify(x => x.Set<Case>().Update(It.IsAny<Case>()), Times.Once);
        mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Ignore("Due date")]
    [TestCase(null, null, null, null, null, null, null)]
    public async Task GetByQueryRequestAsync_WhenCalledWithParameters_ReturnsExpectedCases(int? pageNumber, int? pageSize, string? searchString, string? columnName, FilterOptions? filterOptions, string? filterValue, SortOrders? sortOrders)
    {
        // Arrange
        var caseListResponseExpected = CaseMother.GetCaseList(CommonConst.MaxCount);
        var filterParams = FilterParamsMother.GetFilterParams(columnName!, filterOptions ?? FilterOptions.IsNotEmpty, filterValue!);
        var sortingParams = SortingParamsMother.GetSortingParams(columnName!, sortOrders ?? SortOrders.Asc);
        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, searchString, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<Case>.Create(caseListResponseExpected, queryRequest);

        mockAppDbContext.Setup(x => x.Set<Case>()).ReturnsDbSet(caseListResponseExpected);

        // Act
        var queryResult = await caseRepository.GetByQueryRequestAsync(queryRequest);

        // Asserts
        queryResult.Should().NotBeNull();
        queryResult.Items.Should().NotBeNull();
        queryResult.PaginationData.Should().NotBeNull();
        queryResult.Items.Should().BeEquivalentTo(queryResultExpected.Items);
        queryResult.PaginationData.Should().BeEquivalentTo(queryResultExpected.PaginationData);

        mockAppDbContext.Verify(x => x.Set<Case>(), Times.Once);
    }
}