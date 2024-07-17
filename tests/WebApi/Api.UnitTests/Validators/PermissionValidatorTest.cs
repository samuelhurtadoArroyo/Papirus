namespace Papirus.WebApi.Api.UnitTests.Validators;

[ExcludeFromCodeCoverage]
[TestFixture]
public class PermissionValidatorTest
{
    private PermissionValidator permissionValidator = null!;

    [SetUp]
    public void SetUp()
    {
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es");
        permissionValidator = new PermissionValidator();
    }

    [Test]
    public void PermissionValidator_Validate_WhenFieldsAreValid_ReturnsSuccess()
    {
        // Arrange
        var permissionDto = PermissionDtoMother.DefaultPermission();

        // Act
        var result = permissionValidator.TestValidate(permissionDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public void PermissionValidator_Validate_WhenRequiredFieldsAreMissing_ReturnsValidationErrors()
    {
        // Arrange
        var permissionDto = PermissionDtoMother.GetEmptyPermission();

        // Act
        var result = permissionValidator.TestValidate(permissionDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Count.Should().Be(2);

        result.ShouldHaveValidationErrorFor(m => m.Name)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage("'Nombre' no debería estar vacío.");

        result.ShouldHaveValidationErrorFor(m => m.Description)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage("'Descripción' no debería estar vacío.");
    }

    [Test]
    public void PermissionValidator_Validate_WhenFieldsExceedMaxLength_ReturnsValidationErrors()
    {
        // Arrange
        var permissionDto = PermissionDtoMother.GetPermissionWithMaxLengths();

        // Act
        var result = permissionValidator.TestValidate(permissionDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Count.Should().Be(2);

        result.ShouldHaveValidationErrorFor(m => m.Name)
            .WithErrorCode("MaximumLengthValidator")
            .WithErrorMessage($"'Nombre' debe ser menor o igual que {ValidationConst.MaxFieldLength} caracteres. Ingresó {permissionDto.Name.Length} caracteres.");

        result.ShouldHaveValidationErrorFor(m => m.Description)
            .WithErrorCode("MaximumLengthValidator")
            .WithErrorMessage($"'Descripción' debe ser menor o igual que {ValidationConst.MaxFieldLongLength} caracteres. Ingresó {permissionDto.Description.Length} caracteres.");
    }
}