namespace Papirus.WebApi.Api.UnitTests.Validators;

[ExcludeFromCodeCoverage]
[TestFixture]
public class FirmValidatorTests
{
    private FirmValidator firmValidator = null!;

    [SetUp]
    public void SetUp()
    {
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es");
        firmValidator = new FirmValidator();
    }

    [Test]
    public void FirmValidator_Validate_WhenFieldsAreValid_ReturnsSuccess()
    {
        // Arrange
        var firmDto = FirmDtoMother.GetDefaultFirm();

        // Act
        var result = firmValidator.TestValidate(firmDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public void FirmValidator_Validate_WhenRequiredFieldsAreMissing_ReturnsValidationErrors()
    {
        // Arrange
        var firmDto = FirmDtoMother.GetEmptyFirm();

        // Act
        var result = firmValidator.TestValidate(firmDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Count.Should().Be(2);

        result.ShouldHaveValidationErrorFor(m => m.Name)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage("'Nombre' no debería estar vacío.");

        result.ShouldHaveValidationErrorFor(m => m.GuidIdentifier)
            .WithErrorCode("PredicateValidator")
            .WithErrorMessage("'Identificador GUID' no cumple con la condición especificada.");
    }

    [Test]
    public void FirmValidator_Validate_WhenFieldsExceedMaxLength_ReturnsValidationErrors()
    {
        // Arrange
        var firmDto = FirmDtoMother.GetFirmWithMaxLengths();

        // Act
        var result = firmValidator.TestValidate(firmDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Count.Should().Be(1);

        result.ShouldHaveValidationErrorFor(m => m.Name)
            .WithErrorCode("MaximumLengthValidator")
            .WithErrorMessage($"'Nombre' debe ser menor o igual que {ValidationConst.MaxFieldLength} caracteres. Ingresó {firmDto.Name.Length} caracteres.");
    }

    [Test]
    public void FirmValidator_Validate_WhenGuidIdentifierIsEmpty_ReturnsValidationError()
    {
        // Arrange
        var firmDto = FirmDtoMother.GetDefaultFirm();
        firmDto.GuidIdentifier = Guid.Empty;

        // Act
        var result = firmValidator.TestValidate(firmDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Count.Should().Be(1);

        result.ShouldHaveValidationErrorFor(m => m.GuidIdentifier)
            .WithErrorCode("PredicateValidator")
            .WithErrorMessage("'Identificador GUID' no cumple con la condición especificada.");
    }
}