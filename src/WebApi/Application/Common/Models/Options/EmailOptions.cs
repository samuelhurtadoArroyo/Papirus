namespace Papirus.WebApi.Application.Common.Models.Options;

[ExcludeFromCodeCoverage]
public class EmailOptions
{
    public string SmtpServer { get; set; } = string.Empty;

    public string ImapServer { get; set; } = string.Empty;

    public int SmtpPort { get; set; }

    public int ImapPort { get; set; }

    public string UserName { get; set; } = string.Empty;

    public TimeSpan IdleCheckInterval { get; set; }

    public List<string> AutoAdmiteKeywords { get; set; } = [];

    public string EmailBodyIdentification { get; set; } = string.Empty;
}