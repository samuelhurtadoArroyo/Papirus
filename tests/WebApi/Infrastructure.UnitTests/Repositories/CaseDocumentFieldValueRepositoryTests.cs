namespace Papirus.WebApi.Infrastructure.Repositories.Tests;

[ExcludeFromCodeCoverage]
public class CaseDocumentFieldValueRepositoryTests
{
    private List<CaseDocumentFieldValue> _caseDocumentFieldValueList = null!;

    private CaseDocumentFieldValueRepository _repository = null!;

    private Mock<AppDbContext> _mockAppDbContext = null!;

    private static EquivalencyAssertionOptions<CaseDocumentFieldValue> ExcludeProperties(EquivalencyAssertionOptions<CaseDocumentFieldValue> options)
    {
        return options;
    }

    [SetUp]
    public void SetUp()
    {
        _caseDocumentFieldValueList = CaseDocumentFieldValueMother.GetFieldValueList();
        _mockAppDbContext = new Mock<AppDbContext>();
        _mockAppDbContext.Setup(x => x.Set<CaseDocumentFieldValue>()).ReturnsDbSet(_caseDocumentFieldValueList);
        _mockAppDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        _repository = new CaseDocumentFieldValueRepository(_mockAppDbContext.Object);
    }

    [Test]
    public async Task AddAsync_WhenCalled_AddsCaseDocumentFieldValueSuccessfully()
    {
        // Arrange
        CaseDocumentFieldValue caseDocumentFieldValueResponseExpected = CaseDocumentFieldValueMother.DefaultFieldValue();
        var caseDocumentFieldValueRequest = caseDocumentFieldValueResponseExpected;

        // Act
        var caseDocumentFieldValueResult = await _repository.AddAsync(caseDocumentFieldValueRequest);

        // Assert
        caseDocumentFieldValueResult.Should().NotBeNull();
        caseDocumentFieldValueResult.Should().BeEquivalentTo(caseDocumentFieldValueResponseExpected);

        _mockAppDbContext.Verify(x => x.Set<CaseDocumentFieldValue>(), Times.Once);
        _mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task FindAsync_WhenCalled_ReturnsMatchingCaseDocumentFieldValues()
    {
        // Arrange
        var caseDocumentFieldValueListResponseExpected = CaseDocumentFieldValueMother.GetFieldValueList().Where(x => x.Id > 1);

        // Act
        var caseDocumentFieldValueListResult = await _repository.FindAsync(x => x.Id > 1);

        // Asserts
        caseDocumentFieldValueListResult.Should().NotBeNull();
        caseDocumentFieldValueListResult.Should().BeEquivalentTo(caseDocumentFieldValueListResponseExpected, ExcludeProperties);

        _mockAppDbContext.Verify(x => x.Set<CaseDocumentFieldValue>(), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_WhenCalled_ReturnsAllCaseDocumentFieldValues()
    {
        // Arrange
        var caseDocumentFieldValueListResponseExpected = CaseDocumentFieldValueMother.GetFieldValueList();

        // Act
        var caseDocumentFieldValueListResult = await _repository.GetAllAsync();

        // Asserts
        caseDocumentFieldValueListResult.Should().NotBeNull();
        caseDocumentFieldValueListResult.Should().BeEquivalentTo(caseDocumentFieldValueListResponseExpected, ExcludeProperties);

        _mockAppDbContext.Verify(x => x.Set<CaseDocumentFieldValue>(), Times.Once);
    }

    [Test]
    public async Task GetByCaseIdAndDocumentTypeIdAsync_WhenCalledWithDocumentTypeId_ReturnsExpectedCaseDocumentFieldValues()
    {
        // Arrange
        int caseId = 1;
        int? documentTypeId = 1;
        var expectedResults = new List<CaseDocumentFieldValue>
        {
            new() { Id = 1, CaseId = caseId, DocumentTypeId = documentTypeId.Value }
        };

        _mockAppDbContext.Setup(x => x.CaseDocumentFieldValues).ReturnsDbSet(expectedResults);

        // Act
        var results = await _repository.GetByCaseIdAndDocumentTypeIdAsync(caseId, documentTypeId);

        // Assert
        results.Should().NotBeNull();
        results.Should().BeEquivalentTo(expectedResults);
    }

    [Test]
    public async Task GetByCaseIdAndDocumentTypeIdAsync_WhenCalledWithoutDocumentTypeId_ReturnsExpectedCaseDocumentFieldValues()
    {
        // Arrange
        int caseId = 1;
        int? documentTypeId = null;
        var expectedResults = new List<CaseDocumentFieldValue>
        {
            new() { Id = 1, CaseId = caseId }
        };

        _mockAppDbContext.Setup(x => x.CaseDocumentFieldValues).ReturnsDbSet(expectedResults);

        // Act
        var results = await _repository.GetByCaseIdAndDocumentTypeIdAsync(caseId, documentTypeId);

        // Assert
        results.Should().NotBeNull();
        results.Should().BeEquivalentTo(expectedResults);
    }

    [Test]
    public async Task GetByCaseIdAsync_WhenCalled_ReturnsExpectedCaseDocumentFieldValues()
    {
        // Arrange
        int caseId = 1;
        var expectedResults = new List<CaseDocumentFieldValue>
        {
            new() { Id = 1, CaseId = caseId }
        };

        _mockAppDbContext.Setup(x => x.CaseDocumentFieldValues).ReturnsDbSet(expectedResults);

        // Act
        var results = await _repository.GetByCaseIdAsync(caseId);

        // Assert
        results.Should().NotBeNull();
        results.Should().BeEquivalentTo(expectedResults);
    }

    [Test]
    public async Task GetByCaseIdAsync_WhenNoResults_ReturnsEmptyCaseDocumentFieldValues()
    {
        // Arrange
        int caseId = 1;

        _mockAppDbContext.Setup(x => x.CaseDocumentFieldValues).ReturnsDbSet([]);

        // Act
        var results = await _repository.GetByCaseIdAsync(caseId);

        // Assert
        results.Should().NotBeNull();
        results.Should().BeEmpty();
    }
}