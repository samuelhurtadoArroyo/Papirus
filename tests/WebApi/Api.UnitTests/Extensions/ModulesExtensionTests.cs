using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using Papirus.WebApi.Application.Services;
using Papirus.WebApi.Domain.Interfaces.Repositories;
using Papirus.WebApi.Infrastructure.Data;
using Papirus.WebApi.Infrastructure.Repositories;

namespace Papirus.WebApi.Api.Extensions.Tests;

[ExcludeFromCodeCoverage]
public class ModulesExtensionTests
{
    //[Test]
    //public void AddServices_WhenCalled_RegistersExpectedNumberOfServices()
    //{
    //    // Arrange
    //    IServiceCollection serviceCollection = new ServiceCollection();
    //    Mock<IConfiguration> configuration = new Mock<IConfiguration>();
    //    // Act
    //    serviceCollection.AddServices(configuration.Object);

    //    // Asserts
    //    serviceCollection.Count.Should().Be(19);
    //}

    [Test]
    public void AddRepositories_WhenCalled_RegistersExpectedNumberOfRepositories()
    {
        // Arrange
        IServiceCollection serviceCollection = new ServiceCollection();

        // Act
        serviceCollection.AddRepositories();

        // Asserts
        serviceCollection.Count.Should().Be(17);
    }

    [Test]
    public void AddValidators_WhenCalled_RegistersExpectedNumberOfValidators()
    {
        // Arrange
        IServiceCollection serviceCollection = new ServiceCollection();

        // Act
        serviceCollection.AddValidators();

        // Asserts
        serviceCollection.Count.Should().Be(12);
    }

    [Test]
    public void AddInfrastructureServices_WithConfiguration_RegistersExpectedServiceCount()
    {
        // Arrange
        IServiceCollection serviceCollection = new ServiceCollection();
        IConfiguration configuration = ConfigurationMother.Default();

        // Act
        serviceCollection.AddInfrastructureServices(configuration);

        // Asserts
        serviceCollection.Count.Should().Be(44); // This is the number of Services added by Appinsghts
    }

    [Test]
    public void AddMapping_WhenCalled_RegistersSingleMappingService()
    {
        // Arrange
        IServiceCollection serviceCollection = new ServiceCollection();

        IConfiguration configuration = ConfigurationMother.Default();

        serviceCollection.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        serviceCollection.AddHttpContextAccessor();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<ICurrentUserService, CurrentUserService>();

        // Act
        serviceCollection.AddMapping();

        // Asserts
        serviceCollection.Count.Should().Be(8);
    }
}