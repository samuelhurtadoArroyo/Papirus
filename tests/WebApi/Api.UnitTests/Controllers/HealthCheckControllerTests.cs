namespace Papirus.WebApi.Api.Controllers.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class HealthCheckControllerTests
{
    private HealthCheckController _healthCheckController = null!;

    [SetUp]
    public void SetUp()
    {
        _healthCheckController = new HealthCheckController();
    }

    [Test]
    public void Get_WhenCalled_ReturnsOkWithHealthyStatus()
    {
        // Act
        var response = _healthCheckController.Get() as OkObjectResult;

        // Assert
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        response!.Value.Should().NotBeNull();
        response!.Value.Should().Be("Healthy");
    }
}