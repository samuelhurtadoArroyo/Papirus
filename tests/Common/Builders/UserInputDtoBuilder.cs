namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class UserInputDtoBuilder
{
    private int _id;

    private string _email;

    private string? _firstName;

    private string? _lastName;

    private string _password;

    private int? _roleId;

    public UserInputDtoBuilder()
    {
        _id = 0;
        _email = null!;
        _firstName = null!;
        _lastName = null!;
        _password = null!;
        _roleId = 0;
    }

    public UserInputDtoBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public UserInputDtoBuilder WithFirstName(string? firstName)
    {
        _firstName = firstName;
        return this;
    }

    public UserInputDtoBuilder WithLastName(string? lastName)
    {
        _lastName = lastName;
        return this;
    }

    public UserInputDtoBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public UserInputDtoBuilder WithPassword(string password)
    {
        _password = password;
        return this;
    }

    public UserInputDtoBuilder WithRoleId(int? roleId)
    {
        _roleId = roleId;
        return this;
    }

    public UserInputDto Build()
    {
        return new UserInputDto
        {
            Id = _id,
            FirstName = _firstName!,
            LastName = _lastName!,
            Email = _email,
            Password = _password,
            RoleId = _roleId
        };
    }
}