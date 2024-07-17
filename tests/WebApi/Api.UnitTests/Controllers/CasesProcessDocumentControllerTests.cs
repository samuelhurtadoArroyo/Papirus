using FluentAssertions.Equivalency;

namespace Papirus.WebApi.Api.Controllers.Tests;

[ExcludeFromCodeCoverage]
public class CasesProcessDocumentControllerTests
{
    private CaseProcessDocumentsController _caseProcessDocumentController = null!;

    private Mock<ICaseProcessDocumentService> _mockCaseProcessDocumentService = null!;

    private static EquivalencyAssertionOptions<CaseProcessDocumentDto> ExcludeProperties(EquivalencyAssertionOptions<CaseProcessDocumentDto> options)
    {
        return options;
    }

    [SetUp]
    public void Setup()
    {
        _mockCaseProcessDocumentService = new Mock<ICaseProcessDocumentService>();

        _caseProcessDocumentController = new CaseProcessDocumentsController(_mockCaseProcessDocumentService.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Ignore("Due date")]
    [Test]
    public async Task GetByCaseId_WhenIdIsValid_ReturnsOkWithCaseProcessDocuments()
    {
        // Arrange
        var caseProcessDocumentsResponseExpected = CaseProcessDocumentMother.GetCaseProcessDocumentList();
        var caseProcessDocumentsDtoExpected = CaseProcessDocumentDtoMother.GetCaseProcessDocumentList();
        var id = caseProcessDocumentsResponseExpected[0].CaseId;

        _mockCaseProcessDocumentService.Setup(x => x.GetByCaseId(id)).ReturnsAsync(caseProcessDocumentsResponseExpected);

        // Act
        var response = await _caseProcessDocumentController.GetByCaseId(id) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var caseProcessDocumentDtosResponse = response!.Value as List<CaseProcessDocumentDto>;
        caseProcessDocumentDtosResponse.Should().NotBeNull();
        caseProcessDocumentDtosResponse.Should().BeEquivalentTo(caseProcessDocumentsDtoExpected, ExcludeProperties);

        _mockCaseProcessDocumentService.Verify(x => x.GetByCaseId(It.IsAny<int>()), Times.Once());
    }

    [Ignore("Due date")]
    [Test]
    public async Task GetById_WhenIdIsValid_ReturnsOkWithCaseProcessDocument()
    {
        // Arrange
        var caseProcessDocumentResponseExpected = CaseProcessDocumentMother.DemandCaseProcessDocument();
        var caseProcessDocumentDtoExpected = CaseProcessDocumentDtoMother.DemandCaseProcessDocument();
        var id = caseProcessDocumentResponseExpected.Id;

        _mockCaseProcessDocumentService.Setup(x => x.GetById(id)).ReturnsAsync(caseProcessDocumentResponseExpected);

        // Act
        var response = await _caseProcessDocumentController.GetById(id) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var personDtoResponse = response!.Value as CaseProcessDocumentDto;
        personDtoResponse.Should().NotBeNull();
        personDtoResponse.Should().BeEquivalentTo(caseProcessDocumentDtoExpected, ExcludeProperties);

        _mockCaseProcessDocumentService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once());
    }
}