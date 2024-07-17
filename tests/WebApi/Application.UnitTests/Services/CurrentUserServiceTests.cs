using Microsoft.AspNetCore.Http;
using SharpCompress;

namespace Papirus.WebApi.Application.Services.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class CurrentUserServiceTests
{
    private CurrentUserService _currentUserService = null!;

    private Mock<IHttpContextAccessor> _HttpContextAccessor = null!;

    private Mock<IUserRepository> _UserRepository = null!;

    [SetUp]
    public void Setup()
    {
        _HttpContextAccessor = new Mock<IHttpContextAccessor>();
        _UserRepository = new Mock<IUserRepository>();
        _currentUserService = new CurrentUserService(_HttpContextAccessor.Object, _UserRepository.Object);
    }

    [Test]
    public async Task GetCurrentUserIdAsync_WhenHttpContextIsNull_ReturnsZero()
    {
        // Arrange
        const int resultExpected = -1;

        _currentUserService = new CurrentUserService(_HttpContextAccessor.Object, _UserRepository.Object);

        // Act
        var caseResult = await _currentUserService.GetCurrentUserIdAsync();

        // Asserts
        caseResult.Should().NotNull();
        caseResult.Should().Be(resultExpected);
    }

    [Test]
    public async Task GetCurrentUserIdAsync_WhenUserIsNull_ReturnsZero()
    {
        // Arrange
        const int resultExpected = -1;

        var context = new DefaultHttpContext
        {
            Connection =
            {
                Id = Guid.NewGuid().ToString()
            }
        };

        _HttpContextAccessor.SetupGet(accessor => accessor.HttpContext).Returns(context);
        _currentUserService = new CurrentUserService(_HttpContextAccessor.Object, _UserRepository.Object);

        // Act
        var caseResult = await _currentUserService.GetCurrentUserIdAsync();

        // Asserts
        caseResult.Should().NotNull();
        caseResult.Should().Be(resultExpected);
    }
}