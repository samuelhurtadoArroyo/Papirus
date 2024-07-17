namespace Papirus.WebApi.Api.UnitTests.Validators;

[ExcludeFromCodeCoverage]
[TestFixture]
public class LoginInputDtoValidatorTests
{
    private LoginInputDtoValidator _loginInputDtoValidator = null!;

    [SetUp]
    public void SetUp()
    {
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es");

        _loginInputDtoValidator = new LoginInputDtoValidator();
    }

    [Test]
    public void LoginInputDtoValidator_Validate_WhenFieldsAreValid_ReturnsSuccess()
    {
        // Arrange
        var authenticatonDto = LoginInputDtoMother.CreateBasicUserLoginInputDto();

        // Act
        var result = _loginInputDtoValidator.TestValidate(authenticatonDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public void LoginInputDtoValidator_Validate_WhenRequiredFieldsAreMissing_ReturnsValidationErrors()
    {
        // Arrange
        var authenticatonDto = LoginInputDtoMother.CreateEmptyLoginInputDto();

        // Act
        var result = _loginInputDtoValidator.TestValidate(authenticatonDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Count.Should().Be(2);

        result.ShouldHaveValidationErrorFor(m => m.Email)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage("'Correo Electrónico' no debería estar vacío.");

        result.ShouldHaveValidationErrorFor(m => m.Password)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage("'Contraseña' no debería estar vacío.");
    }

    [Test]
    public void LoginInputDtoValidator_Validate_WhenFieldsExceedMaxLength_ReturnsValidationErrors()
    {
        // Arrange
        var authenticationDto = LoginInputDtoMother.CreateLoginInputDtoWithMaxLengthFields();

        // Act
        var result = _loginInputDtoValidator.TestValidate(authenticationDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Count.Should().Be(2);

        result.ShouldHaveValidationErrorFor(m => m.Email)
            .WithErrorCode("MaximumLengthValidator")
            .WithErrorMessage($"'Correo Electrónico' debe ser menor o igual que {ValidationConst.MaxEmailLength} caracteres. Ingresó {authenticationDto.Email.Length} caracteres.");

        result.ShouldHaveValidationErrorFor(m => m.Password)
            .WithErrorCode("MaximumLengthValidator")
            .WithErrorMessage($"'Contraseña' debe ser menor o igual que {ValidationConst.MaxFieldLength} caracteres. Ingresó {authenticationDto.Password.Length} caracteres.");
    }

    [Test]
    public void LoginInputDtoValidator_Validate_WhenEmailFormatIsInvalid_ReturnsValidationError()
    {
        // Arrange
        var authenticationDto = LoginInputDtoMother.CreateLoginInputDtoWithInvalidEmail();

        // Act
        var result = _loginInputDtoValidator.TestValidate(authenticationDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Count.Should().Be(1);

        result.ShouldHaveValidationErrorFor(m => m.Email)
            .WithErrorCode("EmailValidator")
            .WithErrorMessage("'Correo Electrónico' no es una dirección de correo electrónico válida.");
    }
}