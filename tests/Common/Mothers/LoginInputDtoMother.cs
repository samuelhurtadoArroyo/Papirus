namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class LoginInputDtoMother
{
    public static LoginInputDto Create(string email, string password)
    {
        return new LoginInputDtoBuilder()
            .WithEmail(email)
            .WithPassword(password)
            .Build();
    }

    public static LoginInputDto CreateBasicUserLoginInputDto()
    {
        return Create("Basic.User@email.com", "Password*01");
    }

    public static LoginInputDto CreateEmptyLoginInputDto()
    {
        return Create(null!, null!);
    }

    public static LoginInputDto CreateLoginInputDtoWithMaxLengthFields()
    {
        var maxEmailLength = $"{"A".PadRight(200, 'A')}@{"B".PadRight(100, 'B')}.com";
        var maxPassword = "A".PadRight(ValidationConst.MaxFieldLength + 1, 'A');

        return Create(email: maxEmailLength, password: maxPassword);
    }

    public static LoginInputDto CreateLoginInputDtoWithInvalidEmail()
    {
        return Create("THIS IS AN INVALID EMAIL", "Password*01");
    }
}