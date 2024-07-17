using System.Security.Claims;

namespace Papirus.WebApi.Api.Controllers.Tests;

[ExcludeFromCodeCoverage]
public class CaseDocumentFieldValueControllerTests
{
    private CaseDocumentFieldValueController _controller = null!;

    private Mock<ICaseDocumentFieldValueService> _mockCaseService = null!;

    [SetUp]
    public void Setup()
    {
        _mockCaseService = new Mock<ICaseDocumentFieldValueService>();

        var headers = new HeaderDictionary();

        var response = new Mock<HttpResponse>();
        response.SetupGet(r => r.Headers).Returns(headers);

        var httpContext = new Mock<HttpContext>();
        httpContext.SetupGet(hc => hc.Response).Returns(response.Object);

        _controller = new CaseDocumentFieldValueController(_mockCaseService.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = httpContext.Object
            }
        };

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new ("firmId", "1"),
            new ("roleId", "1")
        }));

        httpContext.Setup(hc => hc.User).Returns(claimsPrincipal);
    }

    [Test]
    public async Task GetByCaseId_WhenDocumentTypeIdIsProvided_ReturnsOkWithFilteredResults()
    {
        // Arrange
        const int caseId = 1;
        int? documentTypeId = 2;
        var expectedResults = new List<CaseDocumentFieldValueDto>
        {
            new() { Id = 1, CaseId = caseId, DocumentTypeId = documentTypeId.Value }
        };

        _mockCaseService.Setup(x => x.GetByCaseIdAndDocumentTypeIdAsync(caseId, documentTypeId)).ReturnsAsync(expectedResults);

        // Act
        var response = await _controller.GetByCaseId(caseId, documentTypeId);

        // Assert
        var objectResult = response.Result as ObjectResult;
        objectResult.Should().NotBeNull();
        objectResult!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var results = objectResult.Value as List<CaseDocumentFieldValueDto>;
        results.Should().NotBeNull();
        results.Should().BeEquivalentTo(expectedResults);

        _mockCaseService.Verify(x => x.GetByCaseIdAndDocumentTypeIdAsync(caseId, documentTypeId), Times.Once);
    }

    [Test]
    public async Task GetByCaseId_WhenDocumentTypeIdIsNotProvided_ReturnsOkWithAllResults()
    {
        // Arrange
        const int caseId = 1;
        int? documentTypeId = null;
        var expectedResults = new List<CaseDocumentFieldValueDto>
        {
            new() { Id = 1, CaseId = caseId }
        };

        _mockCaseService.Setup(x => x.GetByCaseIdAsync(caseId)).ReturnsAsync(expectedResults);

        // Act
        var response = await _controller.GetByCaseId(caseId, documentTypeId);

        // Assert
        var objectResult = response.Result as ObjectResult;
        objectResult.Should().NotBeNull();
        objectResult!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var results = objectResult.Value as List<CaseDocumentFieldValueDto>;
        results.Should().NotBeNull();
        results.Should().BeEquivalentTo(expectedResults);

        _mockCaseService.Verify(x => x.GetByCaseIdAsync(caseId), Times.Once);
    }

    [Test]
    public async Task GetByCaseId_WhenDocumentTypeIdIsNotProvided_ReturnsNoElements()
    {
        // Arrange
        const int caseId = 1;
        int? documentTypeId = null;

        _mockCaseService.Setup(x => x.GetByCaseIdAsync(caseId)).ReturnsAsync(new List<CaseDocumentFieldValueDto>());

        // Act
        var response = await _controller.GetByCaseId(caseId, documentTypeId);

        // Assert
        var result = response.Result as OkObjectResult;
        result.Should().NotBeNull();
        result?.StatusCode.Should().Be(StatusCodes.Status200OK);
        result?.ContentTypes.Count.Should().Be(0);

        _mockCaseService.Verify(x => x.GetByCaseIdAsync(caseId), Times.Once);
    }

    [Test]
    public async Task GetByCaseId_WhenNoResultsFound_ReturnsNotFound()
    {
        // Arrange
        const int caseId = 1;
        int? documentTypeId = 2;

        _mockCaseService.Setup(x => x.GetByCaseIdAndDocumentTypeIdAsync(caseId, documentTypeId)).ReturnsAsync(new List<CaseDocumentFieldValueDto>());

        // Act
        var response = await _controller.GetByCaseId(caseId, documentTypeId);

        // Assert
        var result = response.Result as NotFoundResult;
        result.Should().NotBeNull();

        _mockCaseService.Verify(x => x.GetByCaseIdAndDocumentTypeIdAsync(caseId, documentTypeId), Times.Once);
    }

    [Test]
    public async Task Put_WhenNoResultsFound_ReturnsNotFound()
    {
        // Arrange
        List<UpdateCaseDocumentFieldValueDto> updateDtos = [];

        _mockCaseService.Setup(x => x.UpdateCaseDocumentFieldValues(updateDtos)).ReturnsAsync(false);

        // Act
        var response = await _controller.Put(updateDtos);

        // Assert
        var result = response as NotFoundResult;
        result.Should().NotBeNull();
        result?.StatusCode.Should().Be(new NotFoundResult().StatusCode);

        _mockCaseService.Verify(x => x.UpdateCaseDocumentFieldValues(It.IsAny<List<UpdateCaseDocumentFieldValueDto>>()), Times.Once);
    }

    [Test]
    public async Task Put_WhenNoContent_ReturnsNoContent()
    {
        // Arrange
        List<UpdateCaseDocumentFieldValueDto> updateDtos = [];

        _mockCaseService.Setup(x => x.UpdateCaseDocumentFieldValues(updateDtos)).ReturnsAsync(true);

        // Act
        var response = await _controller.Put(updateDtos);

        // Assert
        var result = response as NoContentResult;
        result.Should().NotBeNull();
        result?.StatusCode.Should().Be(new NoContentResult().StatusCode);

        _mockCaseService.Verify(x => x.UpdateCaseDocumentFieldValues(It.IsAny<List<UpdateCaseDocumentFieldValueDto>>()), Times.Once);
    }
}