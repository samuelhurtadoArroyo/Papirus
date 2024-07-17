namespace Papirus.WebApi.Api.Extensions.Tests;

[ExcludeFromCodeCoverage]
[TestFixture()]
public class HostBuilderExtensionsTests
{
    private IConfiguration configuration = null!;

    [SetUp]
    public void SetUp()
    {
        configuration = ConfigurationMother.Default();
    }

    [Test()]
    public void AddSerilog_WhenCalled_AddsSerilogToServices()
    {
        // Arrange
        var builder = WebApplication.CreateBuilder();
        var initialServices = builder.Services.Count;

        // Act
        builder.Host.AddSerilog(configuration);

        // Asseryts
        builder.Should().NotBeNull();
        var configuredServices = builder.Services.Count - initialServices;
        configuredServices.Should().Be(3);
    }
}