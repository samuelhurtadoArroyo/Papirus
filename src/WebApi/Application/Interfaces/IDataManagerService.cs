namespace Papirus.WebApi.Application.Interfaces;

public interface IDataManagerService
{
    Task<string> UploadFileAsync(FileAttachment fileAttachment, string containerRoute);
}