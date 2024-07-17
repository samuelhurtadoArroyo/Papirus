namespace Papirus.WebApi.Application.Interfaces;

public interface IHtmlToTextConverter
{
    string ConvertHtmlToPlainText(string htmlContent);
}