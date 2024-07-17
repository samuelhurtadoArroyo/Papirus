namespace Papirus.WebApi.Application.Common.Models;

[ExcludeFromCodeCoverage]
public class EmailMessage
{
    public string From { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public string HtmlBody { get; set; } = string.Empty;

    public string TextBody { get; set; } = string.Empty;

    public List<FileAttachment> Attachments { get; set; } = [];
}