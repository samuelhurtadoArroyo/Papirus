using Papirus.WebApi.Application.Common.Models;

namespace Papirus.WebApi.Infrastructure.Common.Mappings;

[ExcludeFromCodeCoverage]
public static class MappingExtensions
{
    public static EmailMessage ConvertToEmailMessage(this MimeMessage mimeMessage)
    {
        string fromAddresses = string.Join(", ", mimeMessage.From.Mailboxes.Select(mb => $"{mb.Name} <{mb.Address}>"));

        var emailMessage = new EmailMessage
        {
            From = fromAddresses,
            Subject = mimeMessage.Subject,
            HtmlBody = mimeMessage.HtmlBody,
            TextBody = mimeMessage.TextBody
        };

        foreach (var attachment in mimeMessage.Attachments)
        {
            if (attachment is MimePart mimePart)
            {
                using var memoryStream = new MemoryStream();
                mimePart.Content.DecodeTo(memoryStream);
                memoryStream.Position = 0;
                emailMessage.Attachments.Add(new FileAttachment
                {
                    FileName = mimePart.FileName,
                    Content = memoryStream.ToArray()
                });
            }
        }

        return emailMessage;
    }
}