namespace Papirus.WebApi.Api.Validators;

public static class GuidValidator
{
    public static bool IsValidGuid(Guid guidIdentifier)
    {
        return Guid.TryParse(guidIdentifier.ToString(), out Guid guid) && guid != Guid.Empty;
    }
}