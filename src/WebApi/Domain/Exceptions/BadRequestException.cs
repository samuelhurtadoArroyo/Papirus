namespace Papirus.WebApi.Domain.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class BadRequestException : BusinessException
{
    public BadRequestException()
    {
    }

    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }
}