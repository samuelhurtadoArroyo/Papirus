namespace Papirus.WebApi.Api.UnitTests.Validators;

[ExcludeFromCodeCoverage]
[TestFixture]
public class UpdateCaseDocumentFieldValueValidatorTests
{
    private UpdateCaseDocumentFieldValueValidator updateCaseDocumentFieldValueValidator = null!;

    [SetUp]
    public void SetUp()
    {
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es");
        updateCaseDocumentFieldValueValidator = new UpdateCaseDocumentFieldValueValidator();
    }

    [Test]
    public void UpdateCaseDocumentFieldValueValidator_Validate_WhenFieldsAreValid_ReturnsSuccess()
    {
        // Arrange
        var updateCaseDocumentFieldValueDto = UpdateCaseDocumentFieldValueDtoMother.Create(1, "value");

        // Act
        var result = updateCaseDocumentFieldValueValidator.TestValidate(updateCaseDocumentFieldValueDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public void UpdateCaseDocumentFieldValueValidator_Validate_WhenRequiredFieldsAreMissing_ReturnsValidationErrors()
    {
        // Arrange
        var dto = UpdateCaseDocumentFieldValueDtoMother.GetEmptyValue(1);

        // Act
        var result = updateCaseDocumentFieldValueValidator.TestValidate(dto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Count.Should().Be(1);

        result.ShouldHaveValidationErrorFor(m => m.FieldValue)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage("'Field Value' no debería estar vacío.");
    }
}