using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.WebApi.Infrastructure.Repositories.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class CaseProcessDocumentRepositoryTests
{
    private List<CaseProcessDocument> _caseProcessDocumentList = null!;

    private CaseProcessDocumentRepository _caseProcessDocumentRepository = null!;

    private Mock<AppDbContext> _mockAppDbContext = null!;

    private static EquivalencyAssertionOptions<CaseProcessDocument> ExcludeProperties(EquivalencyAssertionOptions<CaseProcessDocument> options)
    {
        return options;
    }

    [SetUp]
    public void SetUp()
    {
        _caseProcessDocumentList = CaseProcessDocumentMother.GetCaseProcessDocumentList();
        _mockAppDbContext = new Mock<AppDbContext>();
        _mockAppDbContext.Setup(x => x.Set<CaseProcessDocument>()).ReturnsDbSet(_caseProcessDocumentList);
        _mockAppDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        _caseProcessDocumentRepository = new CaseProcessDocumentRepository(_mockAppDbContext.Object);
    }

    [Test]
    public async Task AddAsync_WhenDocumentIsValid_ReturnsAddedDocument()
    {
        // Arrange
        CaseProcessDocument caseProcessDocumentResponseExpected = CaseProcessDocumentMother.DemandCaseProcessDocument();
        var caseProcessDocumentRequest = caseProcessDocumentResponseExpected;

        // Act
        var caseProcessDocumentResult = await _caseProcessDocumentRepository.AddAsync(caseProcessDocumentRequest);

        // Assert
        caseProcessDocumentResult.Should().NotBeNull();
        caseProcessDocumentResult.Should().BeEquivalentTo(caseProcessDocumentResponseExpected);

        _mockAppDbContext.Verify(x => x.Set<CaseProcessDocument>(), Times.Once);
        _mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task FindAsync_WhenCalled_ReturnsMatchingDocuments()
    {
        // Arrange
        var caseProcessDocumentListResponseExpected = CaseProcessDocumentMother.GetCaseProcessDocumentList().Where(x => x.Id > 1);

        // Act
        var caseProcessDocumentListResult = await _caseProcessDocumentRepository.FindAsync(x => x.Id > 1);

        // Asserts
        caseProcessDocumentListResult.Should().NotBeNull();
        caseProcessDocumentListResult.Should().BeEquivalentTo(caseProcessDocumentListResponseExpected, ExcludeProperties);

        _mockAppDbContext.Verify(x => x.Set<CaseProcessDocument>(), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_WhenCalled_ReturnsAllDocuments()
    {
        // Arrange
        var caseProcessDocumentListResponseExpected = CaseProcessDocumentMother.GetCaseProcessDocumentList();

        // Act
        var caseProcessDocumentListResult = await _caseProcessDocumentRepository.GetAllAsync();

        // Asserts
        caseProcessDocumentListResult.Should().NotBeNull();
        caseProcessDocumentListResult.Should().BeEquivalentTo(caseProcessDocumentListResponseExpected, ExcludeProperties);

        _mockAppDbContext.Verify(x => x.Set<CaseProcessDocument>(), Times.Once);
    }

    [Test]
    public async Task GetByIdAsync_WhenCalled_ReturnsExpectedDocument()
    {
        // Arrange
        var caseProcessDocumentResponseExpected = CaseProcessDocumentMother.DemandCaseProcessDocument();
        int id = caseProcessDocumentResponseExpected.Id;

        // Act
        var caseProcessDocumentResult = await _caseProcessDocumentRepository.GetByIdAsync(id);

        // Assert
        caseProcessDocumentResult.Should().NotBeNull();
        caseProcessDocumentResult.Should().BeEquivalentTo(caseProcessDocumentResponseExpected, ExcludeProperties);

        _mockAppDbContext.Verify(x => x.Set<CaseProcessDocument>(), Times.Once);
    }

    [Test]
    public async Task RemoveAsync_WhenCalled_DeletesDocumentSuccessfully()
    {
        // Arrange
        var caseProcessDocumentRequest = CaseProcessDocumentMother.DemandCaseProcessDocument();

        // Act
        await _caseProcessDocumentRepository.RemoveAsync(caseProcessDocumentRequest);

        // Assert
        _mockAppDbContext.Verify(x => x.Set<CaseProcessDocument>().Remove(It.IsAny<CaseProcessDocument>()), Times.Once);
        _mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_WhenDocumentIsValid_ReturnsUpdatedDocument()
    {
        // Arrange
        var caseProcessDocumentResponseExpected = CaseProcessDocumentMother.DemandCaseProcessDocument();
        var caseProcessDocumentRequest = caseProcessDocumentResponseExpected;

        // Act
        var personResult = await _caseProcessDocumentRepository.UpdateAsync(caseProcessDocumentRequest);

        // Assert
        personResult.Should().NotBeNull();
        personResult.Should().BeEquivalentTo(caseProcessDocumentResponseExpected);

        _mockAppDbContext.Verify(x => x.Set<CaseProcessDocument>().Update(It.IsAny<CaseProcessDocument>()), Times.Once);
        _mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Ignore("Due date")]
    [TestCase(null, null, null, null, null, null, null)]
    public async Task GetByQueryRequestAsync_WhenCalledWithParameters_ReturnsExpectedDocuments(int? pageNumber, int? pageSize, string? searchString, string? columnName, FilterOptions? filterOptions, string? filterValue, SortOrders? sortOrders)
    {
        // Arrange
        var caseProcessDocumentListResponseExpected = CaseProcessDocumentMother.GetCaseProcessDocumentList(CommonConst.MaxCount);
        var filterParams = FilterParamsMother.GetFilterParams(columnName!, filterOptions ?? FilterOptions.IsNotEmpty, filterValue!);
        var sortingParams = SortingParamsMother.GetSortingParams(columnName!, sortOrders ?? SortOrders.Asc);
        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, searchString, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<CaseProcessDocument>.Create(caseProcessDocumentListResponseExpected, queryRequest);

        _mockAppDbContext.Setup(x => x.Set<CaseProcessDocument>()).ReturnsDbSet(caseProcessDocumentListResponseExpected);

        // Act
        var queryResult = await _caseProcessDocumentRepository.GetByQueryRequestAsync(queryRequest);

        // Asserts
        queryResult.Should().NotBeNull();
        queryResult.Items.Should().NotBeNull();
        queryResult.PaginationData.Should().NotBeNull();
        queryResult.Items.Should().BeEquivalentTo(queryResultExpected.Items);
        queryResult.PaginationData.Should().BeEquivalentTo(queryResultExpected.PaginationData);

        _mockAppDbContext.Verify(x => x.Set<CaseProcessDocument>(), Times.Once);
    }
}