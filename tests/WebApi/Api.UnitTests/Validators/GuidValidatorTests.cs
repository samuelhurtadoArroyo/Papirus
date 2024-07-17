namespace Papirus.WebApi.Api.UnitTests.Validators;

[ExcludeFromCodeCoverage]
[TestFixture]
public class GuidValidatorTests
{
    [TestCase("00000000-0000-0000-0000-000000000000", false)]
    [TestCase("b0e241cd-a613-4d99-879e-0049e1c4482a", true)]
    public void IsValidGuid(Guid guid, bool expectedResult)
    {
        var result = GuidValidator.IsValidGuid(guid);

        result.Should().Be(expectedResult);
    }
}