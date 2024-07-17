namespace Papirus.WebApi.Api.UnitTests.Validators;

[ExcludeFromCodeCoverage]
[TestFixture]
public class TeamValidatorTests
{
    private TeamValidator _teamValidator = null!;

    [SetUp]
    public void SetUp()
    {
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es");
        _teamValidator = new TeamValidator();
    }

    [Test]
    public void TeamValidator_Validate_WhenFieldsAreValid_ReturnsSuccess()
    {
        // Arrange
        var teamDto = TeamDtoMother.DefaultTutelasTeam();

        // Act
        var result = _teamValidator.TestValidate(teamDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public void TeamValidator_Validate_WhenRequiredFieldsAreMissing_ReturnsValidationError()
    {
        // Arrange
        var teamDto = TeamDtoMother.GetEmptyTeam();

        // Act
        var result = _teamValidator.TestValidate(teamDto);

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
    public void TeamValidator_Validate_WhenFieldsExceedMaxLength_ReturnsValidationError()
    {
        // Arrange
        var teamDto = TeamDtoMother.GetTeamWithMaxLengths();

        // Act
        var result = _teamValidator.TestValidate(teamDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Count.Should().Be(1);

        result.ShouldHaveValidationErrorFor(m => m.Name)
            .WithErrorCode("MaximumLengthValidator")
            .WithErrorMessage($"'Nombre' debe ser menor o igual que {ValidationConst.MaxFieldLength} caracteres. Ingresó {teamDto.Name.Length} caracteres.");
    }
}