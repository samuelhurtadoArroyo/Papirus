namespace Papirus.WebApi.Api.UnitTests.Validators;

[ExcludeFromCodeCoverage]
[TestFixture]
public class RoleValidatorTests
{
    private RoleValidator roleValidator = null!;

    [SetUp]
    public void SetUp()
    {
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es");
        roleValidator = new RoleValidator();
    }

    [Test]
    public void RoleValidator_Validate_WhenFieldsAreValid_ReturnsSuccess()
    {
        // Arrange
        var roleDto = RoleDtoMother.DefaultBasicRole();

        // Act
        var result = roleValidator.TestValidate(roleDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public void RoleValidator_Validate_WhenRequiredFieldsAreMissing_ReturnsValidationError()
    {
        // Arrange
        var roleDto = RoleDtoMother.GetEmptyRole();

        // Act
        var result = roleValidator.TestValidate(roleDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Count.Should().Be(1);

        result.ShouldHaveValidationErrorFor(m => m.Name)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage("'Nombre' no debería estar vacío.");
    }

    [Test]
    public void RoleValidator_Validate_WhenFieldsExceedMaxLength_ReturnsValidationError()
    {
        // Arrange
        var roleDto = RoleDtoMother.GetRoleWithMaxLengths();

        // Act
        var result = roleValidator.TestValidate(roleDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Count.Should().Be(1);

        result.ShouldHaveValidationErrorFor(m => m.Name)
            .WithErrorCode("MaximumLengthValidator")
            .WithErrorMessage($"'Nombre' debe ser menor o igual que {ValidationConst.MaxFieldLength} caracteres. Ingresó {roleDto.Name.Length} caracteres.");
    }
}