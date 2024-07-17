namespace Papirus.WebApi.Infrastructure.Data.UnitTests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class AppDbContextTests
{
    [SetUp]
    public void SetUp()
    {
        Environment.SetEnvironmentVariable("ConnectionStrings__DefaultConnection", "Data Source=InMemorySampleDb;Mode=Memory;Cache=Shared");
    }

    [TearDown]
    public void TearDown()
    {
        Environment.SetEnvironmentVariable("ConnectionStrings__DefaultConnection", null);
    }

    [Test]
    public void AppDbContext_WhenConfigured_UsesEnvironmentVariableForConnectionString()
    {
        // Arrange
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // Act
        var context = new AppDbContext(optionsBuilder.Options);

        MethodInfo onConfiguringMethod = typeof(AppDbContext).GetMethod("OnConfiguring", BindingFlags.NonPublic | BindingFlags.Instance)!;
        onConfiguringMethod.Invoke(context, [optionsBuilder]);

        // Assert
        optionsBuilder.Options.Should().NotBeNull();
        optionsBuilder.IsConfigured.Should().BeTrue();
    }
}