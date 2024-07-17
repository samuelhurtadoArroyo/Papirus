using System.Linq.Expressions;

namespace Papirus.WebApi.Application.Services.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class CaseProcessDocumentServiceTests
{
    private CaseProcessDocumentService caseProcessDocumentService = null!;

    private Mock<ICaseProcessDocumentRepository> mockCaseProcessDocumentRepository = null!;

    [SetUp]
    public void Setup()
    {
        mockCaseProcessDocumentRepository = new Mock<ICaseProcessDocumentRepository>();
        caseProcessDocumentService = new CaseProcessDocumentService(mockCaseProcessDocumentRepository.Object);
    }

    [Test]
    public async Task GetByCaseId_WhenCaseIdIsValid_ReturnsDocuments()
    {
        // Arrange
        var caseProcessDocumentListReponseExpected = CaseProcessDocumentMother.GetCaseProcessDocumentList(CommonConst.MinCount);
        int id = caseProcessDocumentListReponseExpected[0].Id;

        mockCaseProcessDocumentRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<CaseProcessDocument, bool>>>())).ReturnsAsync(caseProcessDocumentListReponseExpected);

        // Act
        var caseProcessDocumentResult = await caseProcessDocumentService.GetByCaseId(id);

        // Asserts
        caseProcessDocumentResult.Should().NotBeNull();
        caseProcessDocumentResult.Should().BeEquivalentTo(caseProcessDocumentListReponseExpected);
        mockCaseProcessDocumentRepository.Verify(x => x.FindAsync(It.IsAny<Expression<Func<CaseProcessDocument, bool>>>()), Times.Once);
    }

    [Test]
    public async Task Create_WhenDocumentIsValid_ReturnsCreatedDocument()
    {
        // Arrange
        var caseProcessDocumentResponseExpected = CaseProcessDocumentMother.DemandCaseProcessDocument();
        var caseProcessDocumentRequest = CaseProcessDocumentMother.DemandCaseProcessDocument();
        caseProcessDocumentResponseExpected.Id = 0;

        mockCaseProcessDocumentRepository.Setup(x => x.AddAsync(caseProcessDocumentRequest)).ReturnsAsync(caseProcessDocumentResponseExpected);

        // Act
        var caseProcessDocumentResult = await caseProcessDocumentService.Create(caseProcessDocumentRequest);

        // Asserts
        caseProcessDocumentResult.Should().NotBeNull();
        caseProcessDocumentResult.Should().BeEquivalentTo(caseProcessDocumentResponseExpected);

        mockCaseProcessDocumentRepository.Verify(x => x.AddAsync(It.IsAny<CaseProcessDocument>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenCaseIdIsValid_DeletesDocumentSuccessfully()
    {
        // Arrange
        var caseProcessDocumentResponseExpected = CaseProcessDocumentMother.DemandCaseProcessDocument();
        var caseProcessDocumentRequest = CaseProcessDocumentMother.DemandCaseProcessDocument();
        int id = caseProcessDocumentRequest.Id;

        mockCaseProcessDocumentRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(caseProcessDocumentResponseExpected);
        mockCaseProcessDocumentRepository.Setup(x => x.RemoveAsync(caseProcessDocumentRequest)).Verifiable();

        // Act
        await caseProcessDocumentService.Delete(id);

        // Asserts
        mockCaseProcessDocumentRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockCaseProcessDocumentRepository.Verify(x => x.RemoveAsync(It.IsAny<CaseProcessDocument>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenCaseIdIsInvalid_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 0;
        CaseProcessDocument caseProcessDocumentResponseExpected = null!;

        mockCaseProcessDocumentRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(caseProcessDocumentResponseExpected);

        // Act
        Func<Task> action = async () => await caseProcessDocumentService.Delete(id);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockCaseProcessDocumentRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockCaseProcessDocumentRepository.Verify(x => x.RemoveAsync(It.IsAny<CaseProcessDocument>()), Times.Never);
    }

    [Test]
    public async Task Edit_WhenDocumentIsValid_ReturnsUpdatedDocument()
    {
        // Arrange
        var caseProcessDocumentResponseExpected = CaseProcessDocumentMother.DemandCaseProcessDocument();
        var caseProcessDocumentRequest = caseProcessDocumentResponseExpected;
        var caseProcessDocumentResponse = CaseProcessDocumentMother.DemandCaseProcessDocument();
        int id = caseProcessDocumentResponse.Id;

        mockCaseProcessDocumentRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(caseProcessDocumentResponse);
        mockCaseProcessDocumentRepository.Setup(x => x.UpdateAsync(caseProcessDocumentRequest)).ReturnsAsync(caseProcessDocumentResponseExpected);

        // Act
        var firmResult = await caseProcessDocumentService.Edit(caseProcessDocumentRequest);

        // Asserts
        firmResult.Should().NotBeNull();
        firmResult.Should().BeEquivalentTo(caseProcessDocumentResponseExpected);
        mockCaseProcessDocumentRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockCaseProcessDocumentRepository.Verify(x => x.UpdateAsync(It.IsAny<CaseProcessDocument>()), Times.Once);
    }

    [Test]
    public async Task Edit_WhenCaseIdIsInvalid_ThrowsNotFoundException()
    {
        // Arrange
        CaseProcessDocument caseProcessDocumentResponseExpected = null!;
        var caseProcessDocumentRequest = CaseProcessDocumentMother.DemandCaseProcessDocument();
        int id = caseProcessDocumentRequest.Id;

        mockCaseProcessDocumentRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(caseProcessDocumentResponseExpected);

        // Act
        Func<Task> action = async () => await caseProcessDocumentService.Edit(caseProcessDocumentRequest);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockCaseProcessDocumentRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockCaseProcessDocumentRepository.Verify(x => x.UpdateAsync(It.IsAny<CaseProcessDocument>()), Times.Never);
    }

    [Test]
    public async Task GetAll_WhenCalled_ReturnsAllCaseProcessDocuments()
    {
        // Arange
        var caseProcessDocumentListReponseExpected = CaseProcessDocumentMother.GetCaseProcessDocumentList(CommonConst.MinCount);

        mockCaseProcessDocumentRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(caseProcessDocumentListReponseExpected);

        // Act
        var resultList = await caseProcessDocumentService.GetAll() as List<CaseProcessDocument>;

        //Asserts
        resultList.Should().NotBeNull();
        resultList.Should().BeEquivalentTo(caseProcessDocumentListReponseExpected);

        mockCaseProcessDocumentRepository.Verify(x => x.GetAllAsync(), Times.Once);
    }

    [Test]
    public async Task GetById_WhenCaseIdIsValid_ReturnsCaseProcessDocument()
    {
        // Arrange
        var caseProcessDocumentResponseExpected = CaseProcessDocumentMother.DemandCaseProcessDocument();
        int id = caseProcessDocumentResponseExpected.Id;

        mockCaseProcessDocumentRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(caseProcessDocumentResponseExpected);

        // Act
        var caseProcessDocumentResult = await caseProcessDocumentService.GetById(id);

        // Asserts
        caseProcessDocumentResult.Should().NotBeNull();
        caseProcessDocumentResult.Should().BeEquivalentTo(caseProcessDocumentResponseExpected);
        mockCaseProcessDocumentRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task GetById_WhenCaseIdIsInvalid_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 0;
        CaseProcessDocument caseProcessDocumentResponseExpected = null!;

        mockCaseProcessDocumentRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(caseProcessDocumentResponseExpected);

        // Act
        Func<Task> action = async () => await caseProcessDocumentService.GetById(id);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockCaseProcessDocumentRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task GetByQueryRequestAsync_WhenQueryIsValid_ReturnsQueryResult()
    {
        var queryRequest = QueryRequestMother.DefaultQueryRequest();
        var caseListReponseExpected = CaseProcessDocumentMother.GetCaseProcessDocumentList(CommonConst.MinCount);
        var paginationDataExpected = PaginationDataMother.DefaultPaginationData();
        var queryResultExpected = QueryResultMother<CaseProcessDocument>.Create(caseListReponseExpected, paginationDataExpected);

        mockCaseProcessDocumentRepository.Setup(x => x.GetByQueryRequestAsync(queryRequest)).ReturnsAsync(queryResultExpected);

        // Act
        var queryResult = await caseProcessDocumentService.GetByQueryRequestAsync(queryRequest);

        //Asserts
        queryResult.Should().NotBeNull();
        queryResult.Should().BeEquivalentTo(queryResultExpected);

        mockCaseProcessDocumentRepository.Verify(x => x.GetByQueryRequestAsync(It.IsAny<QueryRequest>()), Times.Once);
    }
}