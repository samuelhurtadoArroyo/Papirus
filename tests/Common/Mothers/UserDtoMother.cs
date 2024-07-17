namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class UserDtoMother
{
    public static UserDto Create(int id, string email, string? firstName, string? lastName, bool isActive, int? roleId, int? firmId)
    {
        return new UserDtoBuilder()
               .WithId(id)
               .WithEmail(email)
               .WithFirstName(firstName)
               .WithLastName(lastName)
               .WithIsActive(isActive)
               .WithRoleId(roleId)
               .WithFirmId(firmId)
               .Build();
    }

    public static UserDto AdminUser()
    {
        return Create(1, "Papirus.Administrator@email.com", "Papirus", "Administrator", true, 1, 1);
    }

    public static UserDto BasicUser()
    {
        return Create(2, "Basic.User@email.com", "Basic", "User", true, 2, 1);
    }

    public static UserDto SuperUser()
    {
        return Create(3, "Super.User@email.com", "Super", "User", true, 3, 1);
    }

    public static UserDto NoConfigUser()
    {
        return Create(4, "NoConfig.User@email.com", "No Config", "User", true, 2, 1);
    }

    public static UserDto GetEmptyUser()
    {
        return Create(1, null!, null, null, true, null, null);
    }

    public static UserDto GetUserWithMaxLengths()
    {
        var maxEmailLength = $"{"A".PadRight(200, 'A')}@{"B".PadRight(100, 'B')}.com";
        var maxField = "A".PadRight(ValidationConst.MaxFieldLength + 1, 'A');

        return Create(1, maxEmailLength, maxField, maxField, true, null, null);
    }

    public static UserDto GetUserWithInvalidEmail()
    {
        return Create(1, "THIS IS A INVALID EMAIL", "Basic", "User", true, 1, 1);
    }

    public static List<UserDto> GetUserList()
    {
        return [
            AdminUser(),
            BasicUser(),
            SuperUser()
        ];
    }
}