using System.Security.Claims;

namespace Papirus.WebApi.Api.Controllers.Tests;

[ExcludeFromCodeCoverage]
public class BusinessLineControllerTests
{
    private BusinessLineController _controller = null!;

    private Mock<IBusinessLineService> _mockBusinessLineService = null!;

    [SetUp]
    public void Setup()
    {
        _mockBusinessLineService = new Mock<IBusinessLineService>();

        var headers = new HeaderDictionary();

        var response = new Mock<HttpResponse>();
        response.SetupGet(r => r.Headers).Returns(headers);

        var httpContext = new Mock<HttpContext>();
        httpContext.SetupGet(hc => hc.Response).Returns(response.Object);

        _controller = new BusinessLineController(_mockBusinessLineService.Object)
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
    public async Task GetAll_WhenResultsFound_ReturnsOkWithResults()
    {
        // Arrange
        var expectedResults = new List<BusinessLineDto>
        {
            new() { Id = 1, Name = "Bancolombia S.A"}
        };

        _mockBusinessLineService.Setup(x => x.GetAllAsync()).ReturnsAsync(expectedResults);

        // Act
        var response = await _controller.GetAll();

        // Assert
        var objectResult = response.Result as ObjectResult;
        objectResult.Should().NotBeNull();
        objectResult!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var results = objectResult.Value as List<BusinessLineDto>;
        results.Should().NotBeNull();
        results.Should().BeEquivalentTo(expectedResults);

        _mockBusinessLineService.Verify(x => x.GetAllAsync(), Times.Once);
    }

    [Test]
    public async Task GetAll_WhenNoResultsFound_ReturnsEmptyList()
    {
        // Arrange
        _mockBusinessLineService.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<BusinessLineDto>());

        // Act
        var response = await _controller.GetAll();

        // Assert
        var objectResult = response.Result as ObjectResult;
        objectResult.Should().NotBeNull();
        objectResult!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var results = objectResult.Value as List<BusinessLineDto>;
        results.Should().NotBeNull();
        _mockBusinessLineService.Verify(x => x.GetAllAsync(), Times.Once);
    }
}