using Papirus.WebApi.Application.Common.Models;
using System.IO.Compression;

namespace Papirus.WebApi.Infrastructure.Services;

[ExcludeFromCodeCoverage]
public class AttachmentExtractor : IAttachmentExtractor
{
    public IEnumerable<FileAttachment> GetAllAttachments(EmailMessage message)
    {
        var attachments = new List<FileAttachment>();

        foreach (var attachment in message.Attachments)
        {
            if (IsCompressedFile(attachment.FileName))
            {
                using var memoryStream = new MemoryStream(attachment.Content);
                var extractedAttachments = ExtractAttachments(memoryStream, attachment.FileName);
                attachments.AddRange(extractedAttachments);
            }
            else
            {
                attachments.Add(new FileAttachment
                {
                    FileName = attachment.FileName,
                    Content = attachment.Content
                });
            }
        }

        return attachments;
    }

    private static bool IsCompressedFile(string fileName)
    {
        return fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase) ||
               fileName.EndsWith(".rar", StringComparison.OrdinalIgnoreCase);
    }

    private static List<FileAttachment> ExtractAttachments(Stream archiveStream, string fileName)
    {
        var attachments = new List<FileAttachment>();

        if (fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
        {
            archiveStream.Position = 0; // Reset the position of the stream to read from the beginning.
            using var archive = new ZipArchive(archiveStream, ZipArchiveMode.Read);
            foreach (var entry in archive.Entries)
            {
                if (!entry.FullName.EndsWith('/')) // Check if the entry is not a directory.
                {
                    using var entryStream = entry.Open();
                    using var extractedStream = new MemoryStream();
                    entryStream.CopyTo(extractedStream);

                    attachments.Add(new FileAttachment
                    {
                        FileName = entry.Name,
                        Content = extractedStream.ToArray()
                    });
                }
            }
        }
        else if (fileName.EndsWith(".rar", StringComparison.OrdinalIgnoreCase))
        {
            using var archive = SharpCompress.Archives.Rar.RarArchive.Open(archiveStream);
            foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
            {
                using var entryStream = entry.OpenEntryStream();
                using var extractedStream = new MemoryStream();
                entryStream.CopyTo(extractedStream);

                string safeFileName = entry.Key ?? "default_filename";

                attachments.Add(new FileAttachment
                {
                    FileName = safeFileName,
                    Content = extractedStream.ToArray()
                });
            }
        }

        return attachments;
    }
}