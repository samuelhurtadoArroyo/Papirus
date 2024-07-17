namespace Papirus.WebApi.Api.UnitTests.Validators;

[ExcludeFromCodeCoverage]
[TestFixture]
public class ActorValidatorTests
{
    private ActorValidator actorValidator = null!;

    [SetUp]
    public void SetUp()
    {
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es");
        actorValidator = new ActorValidator();
    }

    [Test]
    public void ActorValidator_Validate_WhenFieldsAreValid_ReturnsSuccess()
    {
        // Arrange
        var actorDto = ActorDtoMother.ClaimantActor();

        // Act
        var result = actorValidator.TestValidate(actorDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public void ActorValidator_Validate_WhenRequiredFieldsAreMissing_ReturnsValidationErrors()
    {
        // Arrange
        var actorDto = ActorDtoMother.GetEmptyActor();

        // Act
        var result = actorValidator.TestValidate(actorDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Count.Should().Be(4);

        result.ShouldHaveValidationErrorFor(m => m.ActorTypeId)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage("'Tipo de Actor' no debería estar vacío.");

        result.ShouldHaveValidationErrorFor(m => m.PersonId)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage("'Persona' no debería estar vacío.");

        result.ShouldHaveValidationErrorFor(m => m.CaseId)
            .WithErrorCode("NotEmptyValidator")
            .WithErrorMessage("'Caso' no debería estar vacío.");
    }
}