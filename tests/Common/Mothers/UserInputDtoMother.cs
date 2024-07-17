namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class UserInputDtoMother
{
    public static UserInputDto Create(string email, string? firstName, string? lastName, string password)
    {
        return new UserInputDtoBuilder()
               .WithEmail(email)
               .WithFirstName(firstName)
               .WithLastName(lastName)
               .WithPassword(password)
               .Build();
    }

    public static UserInputDto BasicValidUser()
    {
        return Create("Basic.User@email.com", "Basic", "User", "Password123!");
    }

    public static UserInputDto GetEmptyUser()
    {
        return Create("", "", "", "");
    }

    public static UserInputDto GetUserWithMaxLengths()
    {
        var maxLengthEmail = new string('a', ValidationConst.MaxEmailLength) + "@test.com";
        var maxLengthField = new string('A', ValidationConst.MaxFieldLength + 1);

        const string basePassword = "Aa1*";
        var maxLengthPassword = new string('*', ValidationConst.MaxPasswordLength);
        var password = basePassword + maxLengthPassword;

        return Create(maxLengthEmail, maxLengthField, maxLengthField, password);
    }

    public static UserInputDto GetUserWithInvalidEmail()
    {
        return Create("invalid-email", "Basic", "User", "Password123!");
    }

    public static UserInputDto GetUserWithJustOverMaxLengths()
    {
        var overLengthEmail = new string('a', ValidationConst.MaxEmailLength - 11) + "@toolong.com";
        var overLengthField = new string('A', ValidationConst.MaxFieldLength + 1);
        var overLengthPassword = new string('A', ValidationConst.MaxPasswordLength + 1);

        return Create(overLengthEmail, overLengthField, overLengthField, overLengthPassword);
    }
}