namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class UserMother
{
    public static User Create(int id, string email, string firstName, string lastName, string hashedPassword, string salt, DateTime registrationDate, bool isActive, int roleId, int firmId)
    {
        return new UserBuilder()
               .WithId(id)
               .WithEmail(email)
               .WithFirstName(firstName)
               .WithLastName(lastName)
               .WithHashedPassword(hashedPassword)
               .WithSalt(salt)
               .WithRegistrationDate(registrationDate)
               .WithIsActive(isActive)
               .WithRoleId(roleId)
               .WithFirmId(firmId)
               .Build();
    }

    public static User AdminUser()
    {
        return Create(1, "Papirus.Administrator@email.com", "Papirus", "Administrator", "tXJlkMcMxJhHvZ2RK6SQShIrBzWAJjPwkFHQLz23GTY=", "YzbbWdkVjn3JNFe1l/IJmA==", DateTime.Now, true, 1, 1);
    }

    public static User BasicUser()
    {
        return Create(2, "Basic.User@email.com", "Basic", "User", "TOFVyw0h3sWJMLk2s+gAljU0V2iNbgK2xBPWBX2gPsw=", "JMHuzQKqE5CXuOTpPkqjDw==", DateTime.Now, true, 2, 1);
    }

    public static User SuperUser()
    {
        return Create(3, "Super.User@email.com", "Super", "User", "pZTwjSj8Iz7tLE/nKcW8v6Fl89YLPOMiLbQ4KmVNlLk=", "/x2XaXyVuu6cAKTsIQJBgQ==", DateTime.Now, true, 3, 1);
    }

    public static User NoConfigUser()
    {
        return Create(4, "NoConfig.User@email.com", "No Config", "User", "pZTwjSj8Iz7tLE/nKcW8v6Fl89YLPOMiLbQ4KmVNlLk=", "/x2XaXyVuu6cAKTsIQJBgQ==", DateTime.Now, true, 3, 1);
    }

    public static List<User> GetUserList()
    {
        return [
            AdminUser(),
            BasicUser(),
            SuperUser()
        ];
    }

    public static List<User> GetUserList(int quantity)
    {
        var userList = new List<User>()
        {
            Create(0, "", "", "", "", "", DateTime.Now, true, 0, 0)
        };

        var userFaker = new Faker<User>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.Email, f => $"email_{f.IndexFaker}@email.com")
            .RuleFor(o => o.FirstName, f => $"FirstName{f.IndexFaker}")
            .RuleFor(o => o.LastName, f => $"LastName{f.IndexFaker}")
            .RuleFor(o => o.HashedPassword, _ => "TOFVyw0h3sWJMLk2s+gAljU0V2iNbgK2xBPWBX2gPsw=")
            .RuleFor(o => o.Salt, _ => "JMHuzQKqE5CXuOTpPkqjDw==")
            .RuleFor(o => o.RegistrationDate, _ => DateTime.Now)
            .RuleFor(o => o.IsActive, _ => true)
            .RuleFor(o => o.RoleId, _ => 2)
            .RuleFor(o => o.FirmId, _ => 1);

        userList.AddRange(userFaker.Generate(quantity));

        return userList;
    }

    public static List<User> GetRandomUserList(int quantity)
    {
        var userFaker = new Faker<User>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.Email, f => f.Person.Email)
            .RuleFor(o => o.FirstName, f => f.Person.FirstName)
            .RuleFor(o => o.LastName, f => f.Person.LastName)
            .RuleFor(o => o.HashedPassword, _ => "TOFVyw0h3sWJMLk2s+gAljU0V2iNbgK2xBPWBX2gPsw=")
            .RuleFor(o => o.Salt, _ => "JMHuzQKqE5CXuOTpPkqjDw==")
            .RuleFor(o => o.RegistrationDate, _ => DateTime.Now)
            .RuleFor(o => o.IsActive, f => f.Random.Bool())
            .RuleFor(o => o.RoleId, f => f.Random.Int(1, 3))
            .RuleFor(o => o.FirmId, _ => 1);

        return userFaker.Generate(quantity);
    }
}