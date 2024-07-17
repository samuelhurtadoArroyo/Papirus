namespace Papirus.WebApi.Application.Interfaces;

public interface IAttachmentExtractor
{
    IEnumerable<FileAttachment> GetAllAttachments(EmailMessage message);
}