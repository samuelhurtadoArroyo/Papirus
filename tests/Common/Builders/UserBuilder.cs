namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class UserBuilder
{
    private int _id;

    private string _email;

    private string _firstName;

    private string _lastName;

    private string _hashedPassword;

    private string _salt;

    private DateTime _registrationDate;

    private bool _isActive;

    private int _roleId;

    private int _firmId;

    public UserBuilder()
    {
        _id = 0;
        _email = null!;
        _firstName = null!;
        _lastName = null!;
        _hashedPassword = null!;
        _salt = null!;
        _registrationDate = DateTime.Now;
        _isActive = false;
        _roleId = 0;
        _firmId = 0;
    }

    public UserBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public UserBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public UserBuilder WithFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }

    public UserBuilder WithLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }

    public UserBuilder WithHashedPassword(string hashedPassword)
    {
        _hashedPassword = hashedPassword;
        return this;
    }

    public UserBuilder WithSalt(string registrationDate)
    {
        _salt = registrationDate;
        return this;
    }

    public UserBuilder WithRegistrationDate(DateTime registrationDate)
    {
        _registrationDate = registrationDate;
        return this;
    }

    public UserBuilder WithIsActive(bool isActive)
    {
        _isActive = isActive;
        return this;
    }

    public UserBuilder WithRoleId(int firmId)
    {
        _roleId = firmId;
        return this;
    }

    public UserBuilder WithFirmId(int firmId)
    {
        _firmId = firmId;
        return this;
    }

    public User Build()
    {
        return new User
        {
            Id = _id,
            Email = _email,
            FirstName = _firstName,
            LastName = _lastName,
            HashedPassword = _hashedPassword,
            Salt = _salt,
            RegistrationDate = _registrationDate,
            IsActive = _isActive,
            RoleId = _roleId,
            FirmId = _firmId,
        };
    }
}