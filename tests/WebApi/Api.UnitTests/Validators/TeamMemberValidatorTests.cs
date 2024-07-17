namespace Papirus.WebApi.Api.UnitTests.Validators;

[ExcludeFromCodeCoverage]
[TestFixture]
public class TeamMemberValidatorTests
{
    private TeamMemberValidator _teamMemberValidator = null!;

    [SetUp]
    public void SetUp()
    {
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es");
        _teamMemberValidator = new TeamMemberValidator();
    }

    [Test]
    public void TeamMemberValidator_Validate_WhenFieldsAreValid_ReturnsSuccess()
    {
        // Arrange
        var teammemberDto = TeamMemberDtoMother.DefaultTeamMemberLeader();

        // Act
        var result = _teamMemberValidator.TestValidate(teammemberDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Test]
    public void TeamMemberValidator_Validate_WhenRequiredFieldsAreMissing_ReturnsValidationError()
    {
        // Arrange
        var teammemberDto = TeamMemberDtoMother.GetEmptyTeamMember();

        // Act
        var result = _teamMemberValidator.TestValidate(teammemberDto);

        // Asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors.Count.Should().Be(1);
    }
}