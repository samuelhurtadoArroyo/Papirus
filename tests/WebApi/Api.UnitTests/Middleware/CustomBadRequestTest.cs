namespace Papirus.WebApi.Api.UnitTests.Middleware;

[ExcludeFromCodeCoverage]
public class CustomBadRequestTest
{
    private const string errorType = "Bad Request";

    private ActionContext actionContext = null!;

    [SetUp]
    public void Setup()
    {
        actionContext = new ActionContext();
    }

    [Test]
    public void ConstructErrorMessages_WhenModelStateHasOneError_ReturnsSingleError()
    {
        // Arrange
        const string keyError = "name";
        const string errorMessage = $"The {keyError} cannot be null";
        actionContext.ModelState.AddModelError(keyError, errorMessage);

        // Act
        var result = actionContext.ConstructErrorMessages();

        // Asserts
        result.ErrorType.Should().Be(errorType);
        result.Errors?.Count.Should().Be(1);
        result.Errors?[0].Should().Be(errorMessage);
    }

    [Test]
    public void ConstructErrorMessages_WhenModelStateHasTwoErrors_ReturnsMultipleErrors()
    {
        // Arrange
        const string keyError1 = "url";
        const string keyError2 = "name";
        const string errorMessage1 = $"The field {keyError1} is required.";
        const string errorMessage2 = $"The field {keyError2} is required.";
        actionContext.ModelState.AddModelError(keyError1, errorMessage1);
        actionContext.ModelState.AddModelError(keyError2, errorMessage2);

        // Act
        var result = actionContext.ConstructErrorMessages();

        // Asserts
        result.ErrorType.Should().Be(errorType);
        result.Errors?.Count.Should().Be(2);
        result.Errors?[0].Should().Be(errorMessage1);
        result.Errors?[1].Should().Be(errorMessage2);
    }
}