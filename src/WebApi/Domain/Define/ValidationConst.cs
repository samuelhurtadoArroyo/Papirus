namespace Papirus.WebApi.Domain.Define;

[ExcludeFromCodeCoverage]
public static class ValidationConst
{
    public static readonly int MaxFieldLength = 50;

    public static readonly int MaxFieldLength128 = 128;

    public static readonly int MaxFieldLongLength = 300;

    public static readonly int MaxEmailLength = 300;

    public static readonly int IdentifierExactLength = 36;

    public static readonly int MinPasswordLength = 8;

    public static readonly int MaxPasswordLength = 64;

    public static readonly int MinUppercaseLetters = 1;

    public static readonly int MinLowercaseLetters = 1;

    public static readonly int MinDigits = 1;

    public static readonly int MinSpecialCharacters = 1;
}