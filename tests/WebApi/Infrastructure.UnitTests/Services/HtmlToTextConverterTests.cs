namespace Papirus.WebApi.Infrastructure.Services.Tests;

[ExcludeFromCodeCoverage]
public class HtmlToTextConverterTests
{
    private HtmlToTextConverter _repository = null!;

    [SetUp]
    public void SetUp()
    {
        _repository = new HtmlToTextConverter();
    }

    [Test]
    public void ConvertHtmlToPlainText_WhenHtmlContentIsEmpty_ReturnsEmpty()
    {
        // Arrange
        string html = string.Empty;

        // Act
        var result = _repository.ConvertHtmlToPlainText(html);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be("");
    }

    [Test]
    public void ConvertHtmlToPlainText_WhenHtmlContentIsValid_ReturnsText()
    {
        // Arrange
        const string html = "<p>Hello word!!</p>";
        const string text = "Hello word!!";

        // Act
        var result = _repository.ConvertHtmlToPlainText(html);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(text);
    }
}