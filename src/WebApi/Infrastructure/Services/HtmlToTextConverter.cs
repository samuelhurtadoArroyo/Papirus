namespace Papirus.WebApi.Infrastructure.Services;

public class HtmlToTextConverter : IHtmlToTextConverter
{
    public string ConvertHtmlToPlainText(string htmlContent)
    {
        var document = new HtmlDocument();
        document.LoadHtml(htmlContent);

        var plainText = HtmlEntity.DeEntitize(document.DocumentNode.InnerText);
        return RemoveUnnecessaryNewLines(plainText);
    }

    private static string RemoveUnnecessaryNewLines(string text)
    {
        // Replace multiple new lines with a single new line
        return Regex.Replace(text, @"(\r\n|\r|\n){2,}", "\n").Trim();
    }
}