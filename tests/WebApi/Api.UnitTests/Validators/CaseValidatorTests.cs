namespace Papirus.WebApi.Api.UnitTests.Validators;

[ExcludeFromCodeCoverage]
[TestFixture]
public class CaseValidatorTests
{
    private CaseValidator caseValidator = null!;

    [SetUp]
    public void SetUp()
    {
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es");
        caseValidator = new CaseValidator();
    }

    [Ignore("Due date")]
    [Test]
    public void CaseValidator_Validate_WhenFieldsAreValid_ReturnsSuccess()
    {
        // Arrange
        var caseDto = CaseDtoMother.DemandCase();

        // Act
        var result = caseValidator.TestValidate(caseDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public void CaseValidator_Validate_WhenRequiredFieldsAreMissing_ReturnsValidationErrors()
    {
        // Arrange
        var personDto = CaseDtoMother.GetEmptyCase();

        // Act
        var result = caseValidator.TestValidate(personDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Count.Should().Be(5);

        result.ShouldHaveValidationErrorFor(m => m.Court)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage("'Corte' no debería estar vacío.");

        result.ShouldHaveValidationErrorFor(m => m.City)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage("'Ciudad' no debería estar vacío.");

#pragma warning disable S125 // Sections of code should not be commented out

        //result.ShouldHaveValidationErrorFor(m => m.Amount)
        //    .WithErrorCode("NotEmptyValidator")

        //    .WithErrorMessage("'Monto' no debería estar vacío.");

        //result.ShouldHaveValidationErrorFor(m => m.ProcessId)
        //    .WithErrorCode("NotEmptyValidator")
        //    .WithErrorMessage("'Identificador de Proceso' no debería estar vacío.");
#pragma warning restore S125 // Sections of code should not be commented out

        result.ShouldHaveValidationErrorFor(m => m.FilePath)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage("'Ruta de Archivo' no debería estar vacío.");

        result.ShouldHaveValidationErrorFor(m => m.FileName)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage("'Nombre de Archivo' no debería estar vacío.");
    }

    [Test]
    public void CaseValidator_Validate_WhenFieldsExceedMaxLength_ReturnsValidationErrors()
    {
        // Arrange
        var caseDto = CaseDtoMother.GetCaseWithMaxLengths();

        // Act
        var result = caseValidator.TestValidate(caseDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Count.Should().Be(2);

        result.ShouldHaveValidationErrorFor(m => m.Court)
            .WithErrorCode("MaximumLengthValidator")
            .WithErrorMessage($"'Corte' debe ser menor o igual que {ValidationConst.MaxFieldLength128} caracteres. Ingresó 129 caracteres.");

        result.ShouldHaveValidationErrorFor(m => m.City)
            .WithErrorCode("MaximumLengthValidator")
            .WithErrorMessage($"'Ciudad' debe ser menor o igual que {ValidationConst.MaxFieldLength} caracteres. Ingresó 129 caracteres.");
    }
}