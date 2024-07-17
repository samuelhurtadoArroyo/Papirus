namespace Papirus.WebApi.Api.UnitTests.Validators;

[ExcludeFromCodeCoverage]
[TestFixture]
public class UserInputDtoValidatorTests
{
    private UserInputDtoValidator userValidator = null!;

    [SetUp]
    public void SetUp()
    {
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es");
        userValidator = new UserInputDtoValidator();
    }

    [Test]
    public void UserInputDtoValidator_Validate_WhenFieldsAreValid_ReturnsSuccess()
    {
        // Arrange
        var authenticatonDto = UserInputDtoMother.BasicValidUser();

        // Act
        var result = userValidator.TestValidate(authenticatonDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public void UserInputDtoValidator_Validate_WhenRequiredFieldsAreMissing_ReturnsValidationErrors()
    {
        // Arrange
        var userInputDto = UserInputDtoMother.GetEmptyUser();

        // Act
        var result = userValidator.TestValidate(userInputDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Count.Should().Be(10);

        result.ShouldHaveValidationErrorFor(m => m.Email)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage("'Correo Electrónico' no debería estar vacío.");

        result.ShouldHaveValidationErrorFor(m => m.Email)
            .WithErrorCode("EmailValidator")
            .WithErrorMessage("'Correo Electrónico' no es una dirección de correo electrónico válida.");

        result.ShouldHaveValidationErrorFor(m => m.FirstName)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage("'Nombre' no debería estar vacío.");

        result.ShouldHaveValidationErrorFor(m => m.LastName)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage("'Apellido' no debería estar vacío.");

        result.ShouldHaveValidationErrorFor(m => m.Password)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage("'Contraseña' no debería estar vacío.");

        result.ShouldHaveValidationErrorFor(m => m.Password)
            .WithErrorCode("MinimumLengthValidator")
            .WithErrorMessage($"'Contraseña' debe tener al menos 8 caracteres. Ingresó {userInputDto.Password.Length} caracteres.");

        result.ShouldHaveValidationErrorFor(m => m.Password)
            .WithErrorCode("RegularExpressionValidator")
            .WithErrorMessage("'Contraseña' debe contener al menos 1 letra(s) mayúscula(s).");

        result.ShouldHaveValidationErrorFor(m => m.Password)
            .WithErrorCode("RegularExpressionValidator")
            .WithErrorMessage("'Contraseña' debe contener al menos 1 letra(s) minúscula(s).");

        result.ShouldHaveValidationErrorFor(m => m.Password)
            .WithErrorCode("RegularExpressionValidator")
            .WithErrorMessage("'Contraseña' debe contener al menos 1 dígito(s).");

        result.ShouldHaveValidationErrorFor(m => m.Password)
            .WithErrorCode("RegularExpressionValidator")
            .WithErrorMessage("'Contraseña' debe contener al menos 1 carácter(es) especial(es).");
    }

    [Test]
    public void UserInputDtoValidator_Validate_WhenFieldsExceedMaxLength_ReturnsValidationErrors()
    {
        // Arrange
        var userInputDto = UserInputDtoMother.GetUserWithMaxLengths();

        // Act
        var result = userValidator.TestValidate(userInputDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Count.Should().Be(4);

        result.ShouldHaveValidationErrorFor(m => m.Email)
            .WithErrorCode("MaximumLengthValidator")
            .WithErrorMessage($"'Correo Electrónico' debe ser menor o igual que {ValidationConst.MaxEmailLength} caracteres. Ingresó {userInputDto.Email.Length} caracteres.");

        result.ShouldHaveValidationErrorFor(m => m.FirstName)
            .WithErrorCode("MaximumLengthValidator")
            .WithErrorMessage($"'Nombre' debe ser menor o igual que {ValidationConst.MaxFieldLength} caracteres. Ingresó {userInputDto.FirstName.Length} caracteres.");

        result.ShouldHaveValidationErrorFor(m => m.LastName)
            .WithErrorCode("MaximumLengthValidator")
            .WithErrorMessage($"'Apellido' debe ser menor o igual que {ValidationConst.MaxFieldLength} caracteres. Ingresó {userInputDto.LastName.Length} caracteres.");

        result.ShouldHaveValidationErrorFor(m => m.Password)
            .WithErrorCode("MaximumLengthValidator")
            .WithErrorMessage($"'Contraseña' debe ser menor o igual que {ValidationConst.MaxPasswordLength} caracteres. Ingresó {userInputDto.Password.Length} caracteres.");
    }

    [Test]
    public void UserInputDtoValidator_Validate_WhenEmailFormatIsInvalid_ReturnsValidationError()
    {
        // Arrange
        var userInputDto = UserInputDtoMother.GetUserWithInvalidEmail();

        // Act
        var result = userValidator.TestValidate(userInputDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Count.Should().Be(1);

        result.ShouldHaveValidationErrorFor(m => m.Email)
            .WithErrorCode("EmailValidator")
            .WithErrorMessage("'Correo Electrónico' no es una dirección de correo electrónico válida.");
    }

    [Test]
    public void UserInputDtoValidator_Validate_WhenFieldsAreJustOverMaxLength_ReturnsValidationErrors()
    {
        // Arrange
        var userInputDto = UserInputDtoMother.GetUserWithJustOverMaxLengths();

        // Act
        var result = userValidator.TestValidate(userInputDto);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain(e => e.PropertyName == "FirstName" && e.ErrorMessage.Contains("debe ser menor o igual"));
        result.Errors.Should().Contain(e => e.PropertyName == "LastName" && e.ErrorMessage.Contains("debe ser menor o igual"));
        result.Errors.Should().Contain(e => e.PropertyName == "Email" && e.ErrorMessage.Contains("debe ser menor o igual"));
        result.Errors.Should().Contain(e => e.PropertyName == "Password" && e.ErrorMessage.Contains("debe ser menor o igual"));
    }

    [Test]
    public void UserInputDtoValidator_Validate_WhenPasswordMeetsRequirements_ReturnsSuccess()
    {
        // Arrange
        var userInputDto = UserInputDtoMother.BasicValidUser();

        // Act
        var result = userValidator.TestValidate(userInputDto);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public void UserInputDtoValidator_Validate_WhenPasswordFailsRequirements_ReturnsValidationErrors()
    {
        // Arrange
        var userInputDto = UserInputDtoMother.Create("valid@email", "fname", "lname", "abc");

        // Act
        var result = userValidator.TestValidate(userInputDto);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Should().Contain(e => e.ErrorCode == "MinimumLengthValidator" && e.ErrorMessage.Equals($"'Contraseña' debe tener al menos 8 caracteres. Ingresó {userInputDto.Password.Length} caracteres."));
        result.Errors.Should().Contain(e => e.ErrorCode == "RegularExpressionValidator" && e.ErrorMessage.Equals("'Contraseña' debe contener al menos 1 letra(s) mayúscula(s)."));
        result.Errors.Should().Contain(e => e.ErrorCode == "RegularExpressionValidator" && e.ErrorMessage.Equals("'Contraseña' debe contener al menos 1 dígito(s)."));
        result.Errors.Should().Contain(e => e.ErrorCode == "RegularExpressionValidator" && e.ErrorMessage.Equals("'Contraseña' debe contener al menos 1 carácter(es) especial(es)."));
    }
}