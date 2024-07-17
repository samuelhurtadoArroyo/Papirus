namespace Papirus.WebApi.Application.Services.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class AuthenticationServiceTests
{
    private AuthenticationService _authenticationService = null!;

    private IConfiguration _configuration = null!;

    private Mock<IUserRepository> _mockUserRepository = null!;

    [SetUp]
    public void Setup()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _configuration = ConfigurationMother.Default();

        _authenticationService = new AuthenticationService(
            _mockUserRepository.Object,
            _configuration);
    }

    [Test]
    public void GenerateJwtToken_WhenUserIsValid_GeneratesExpectedToken()
    {
        // Arrange
        var user = UserMother.BasicUser();
        var tokenBase = TokenManager.GenerateToken(_configuration, user);
        var payloadExpected = TokenManager.DecodeToken(tokenBase);

        // Act
        var token = _authenticationService.GenerateJwtToken(user);

        // Asserts
        token.Should().NotBeNull();
        var payloadResult = TokenManager.DecodeToken(token);
        payloadResult.Should().NotBeNull();
        payloadResult["aud"].Should().Be(payloadExpected["aud"]);
        payloadResult["iss"].Should().Be(payloadExpected["iss"]);
        payloadResult["nameid"].Should().Be(payloadExpected["nameid"]);
        payloadResult["email"].Should().Be(payloadExpected["email"]);
        payloadResult["name"].Should().Be(payloadExpected["name"]);
        payloadResult["firmId"].Should().Be(payloadExpected["firmId"]);
        payloadResult["roleId"].Should().Be(payloadExpected["roleId"]);
    }

    [Test]
    public void GenerateJwtToken_WhenFirmIdIsInvalid_ThrowsInvalidOperationException()
    {
        // Arrange
        var userWithInvalidFirmId = UserMother.BasicUser();
        userWithInvalidFirmId.FirmId = 0;

        // Act & Assert
        Assert.That(() => _authenticationService.GenerateJwtToken(userWithInvalidFirmId),
            Throws.TypeOf<InvalidOperationException>()
                .With.Message.EqualTo("FirmId is required but missing for the user."));
    }

    [Test]
    public async Task Login_WhenCredentialsAreValid_AuthenticatesUserSuccessfully()
    {
        // Arrange
        var authenticationDto = LoginInputDtoMother.CreateBasicUserLoginInputDto();
        var userExpected = UserMother.BasicUser();

        _mockUserRepository.Setup(x => x.FindByEmailAsync(authenticationDto.Email)).ReturnsAsync(userExpected);

        // Act
        var loginResult = await _authenticationService.Login(authenticationDto.Email, authenticationDto.Password);

        // Asserts
        loginResult.Should().NotBeNull();
    }

    [Test]
    public async Task Login_WhenEmailDoesNotExist_ReturnsEmptyToken()
    {
        // Arrange
        var basicUser = UserMother.BasicUser();
        const string password = "dummyPassword";

        _mockUserRepository.Setup(x => x.FindByEmailAsync(basicUser.Email)).ReturnsAsync((User?)null);

        // Act
        var token = await _authenticationService.Login(basicUser.Email, password);

        // Assert
        token.Should().Be(string.Empty);

        _mockUserRepository.Verify(x => x.FindByEmailAsync(basicUser.Email), Times.Once);
    }

    [Test]
    public async Task Login_WhenPasswordIsIncorrect_ReturnsEmptyToken()
    {
        // Arrange
        var basicUser = UserMother.BasicUser();
        const string incorrectPassword = "thisIsTheWrongPassword"; // Make sure this password is different from the one used in UserWithIncorrectPassword method.

        _mockUserRepository.Setup(x => x.FindByEmailAsync(basicUser.Email)).ReturnsAsync(basicUser);

        // Act
        var token = await _authenticationService.Login(basicUser.Email, incorrectPassword);

        // Assert
        token.Should().Be(string.Empty);

        _mockUserRepository.Verify(x => x.FindByEmailAsync(basicUser.Email), Times.Once);
    }

    [Test]
    public async Task Register_WhenUserIsNew_CreatesUserSuccessfully()
    {
        // Arrange
        var user = UserMother.BasicUser();
        user.Id = 0;
        var password = "Password*01";
        var userExpected = UserMother.BasicUser();
        User userNull = null!;

        _mockUserRepository.Setup(x => x.FindByEmailAsync(user.Email)).ReturnsAsync(userNull);
        _mockUserRepository.Setup(x => x.AddAsync(It.IsAny<User>())).ReturnsAsync(userExpected);

        // Act
        var userResult = await _authenticationService.Register(user, password, It.IsAny<int>());

        // Asserts
        userResult.Should().NotBeNull();
        userResult.Should().BeEquivalentTo(userExpected);

        _mockUserRepository.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Once);
        _mockUserRepository.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
    }

    [Test]
    public async Task Register_WhenUserIsNewWithoutConfiguration_CreatesUserSuccessfully()
    {
        // Arrange
        var user = UserMother.NoConfigUser();
        var userExpected = UserMother.BasicUser();
        var password = "Password*01";
        User userNull = null!;

        _mockUserRepository.Setup(x => x.FindByEmailAsync(user.Email)).ReturnsAsync(userNull);
        _mockUserRepository.Setup(x => x.AddAsync(It.IsAny<User>())).ReturnsAsync(userExpected);

        // Act
        var userResult = await _authenticationService.Register(user, password, It.IsAny<int>());

        // Asserts
        userResult.Should().NotBeNull();
        userResult.Should().BeEquivalentTo(userExpected);

        _mockUserRepository.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Once);
        _mockUserRepository.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
    }

    [Test]
    public async Task Register_WhenUserExists_ThrowsInternalServerErrorException()
    {
        // Arrange
        var user = UserMother.BasicUser();
        var userExpected = UserMother.BasicUser();
        var password = "Password*01";

        _mockUserRepository.Setup(x => x.FindByEmailAsync(user.Email)).ReturnsAsync(userExpected);

        // Act
        Func<Task> action = async () => await _authenticationService.Register(user, password, It.IsAny<int>());
        await action.Should().ThrowAsync<InternalServerErrorException>().WithMessage("A user with the given email already exists.");

        // Asserts
        _mockUserRepository.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Once);
        _mockUserRepository.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Never);
    }
}