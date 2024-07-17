namespace Papirus.WebApi.Api.Controllers.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class AuthenticationControllerTests
{
    private AuthenticationController _authenticationController = null!;

    private Mock<IAuthenticationService> _mockAuthenticationService = null!;

    private IConfiguration _configuration = null!;

    [SetUp]
    public void SetUp()
    {
        _mockAuthenticationService = new Mock<IAuthenticationService>();
        _configuration = ConfigurationMother.Default();

        _authenticationController = new AuthenticationController(_mockAuthenticationService.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Test]
    public async Task Login_WithValidCredentials_ReturnsOkWithToken()
    {
        // Arrange
        var authenticationDtoRequest = LoginInputDtoMother.CreateBasicUserLoginInputDto();
        var user = UserMother.BasicUser();
        var token = TokenManager.GenerateToken(_configuration, user);
        var loginDtoExpected = new LoginDto
        {
            Token = token
        };

        _mockAuthenticationService.Setup(x => x.Login(authenticationDtoRequest.Email, authenticationDtoRequest.Password))
                                 .ReturnsAsync(token);

        // Act
        var response = await _authenticationController.Login(authenticationDtoRequest) as OkObjectResult;

        // Assert
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var loginDtoResponse = response.Value as LoginDto;
        loginDtoResponse.Should().BeEquivalentTo(loginDtoExpected);

        _mockAuthenticationService.Verify(x => x.Login(authenticationDtoRequest.Email, authenticationDtoRequest.Password), Times.Once());
    }

    [Test]
    public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
    {
        // Arrange
        var authenticationDtoRequest = LoginInputDtoMother.CreateLoginInputDtoWithInvalidEmail();

        var result = string.Empty;

        _mockAuthenticationService.Setup(x => x.Login(authenticationDtoRequest.Email, authenticationDtoRequest.Password))
                                 .ReturnsAsync(result);

        // Act
        var response = await _authenticationController.Login(authenticationDtoRequest) as UnauthorizedResult;

        // Assert
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);

        _mockAuthenticationService.Verify(x => x.Login(authenticationDtoRequest.Email, authenticationDtoRequest.Password), Times.Once());
    }
}