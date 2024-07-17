namespace Papirus.WebApi.Application.Common.Models;

[ExcludeFromCodeCoverage]
public class FileAttachment
{
    public string FileName { get; set; } = string.Empty;

    public byte[] Content { get; set; } = [];
}