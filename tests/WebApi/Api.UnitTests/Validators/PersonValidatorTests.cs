namespace Papirus.WebApi.Api.UnitTests.Validators;

[ExcludeFromCodeCoverage]
[TestFixture]
public class PersonValidatorTests
{
    private PersonValidator personValidator = null!;

    [SetUp]
    public void SetUp()
    {
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es");
        personValidator = new PersonValidator();
    }

    [Test]
    public void PersonValidator_Validate_WhenFieldsAreValid_ReturnsSuccess()
    {
        // Arrange
        var personDto = PersonDtoMother.NaturalCCPerson();

        // Act
        var result = personValidator.TestValidate(personDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public void PersonValidator_Validate_WhenRequiredFieldsAreMissing_ReturnsValidationErrors()
    {
        // Arrange
        var personDto = PersonDtoMother.GetEmptyPerson();

        // Act
        var result = personValidator.TestValidate(personDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Count.Should().Be(4);

        result.ShouldHaveValidationErrorFor(m => m.PersonTypeId)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage("'Tipo de Persona' no debería estar vacío.");

        result.ShouldHaveValidationErrorFor(m => m.Name)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage("'Nombre' no debería estar vacío.");

        result.ShouldHaveValidationErrorFor(m => m.IdentificationTypeId)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage("'Tipo de Identificación' no debería estar vacío.");

        result.ShouldHaveValidationErrorFor(m => m.IdentificationNumber)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage("'Número de Identificación' no debería estar vacío.");
    }

    [Test]
    public void PersonValidator_Validate_WhenFieldsExceedMaxLength_ReturnsValidationErrors()
    {
        // Arrange
        var personDto = PersonDtoMother.GetPersonWithMaxLengths();

        // Act
        var result = personValidator.TestValidate(personDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Count.Should().Be(4);

        result.ShouldHaveValidationErrorFor(m => m.Name)
            .WithErrorCode("MaximumLengthValidator")
            .WithErrorMessage($"'Nombre' debe ser menor o igual que {ValidationConst.MaxFieldLongLength} caracteres. Ingresó 301 caracteres.");

        result.ShouldHaveValidationErrorFor(m => m.IdentificationNumber)
            .WithErrorCode("MaximumLengthValidator")
            .WithErrorMessage($"'Número de Identificación' debe ser menor o igual que {ValidationConst.MaxFieldLength} caracteres. Ingresó 51 caracteres.");
    }
}