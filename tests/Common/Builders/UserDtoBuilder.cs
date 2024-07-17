namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class UserDtoBuilder
{
    private int _id;

    private string _email;

    private string? _firstName;

    private string? _lastName;

    private int? _roleId;

    private bool _isActive;

    private int? _firmId;

    public UserDtoBuilder()
    {
        _id = 0;
        _email = null!;
        _firstName = null!;
        _lastName = null!;
        _isActive = false;
        _roleId = 0;
        _firmId = 0;
    }

    public UserDtoBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public UserDtoBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public UserDtoBuilder WithFirstName(string? firstName)
    {
        _firstName = firstName;
        return this;
    }

    public UserDtoBuilder WithLastName(string? lastName)
    {
        _lastName = lastName;
        return this;
    }

    public UserDtoBuilder WithRoleId(int? roleId)
    {
        _roleId = roleId;
        return this;
    }

    public UserDtoBuilder WithIsActive(bool isActive)
    {
        _isActive = isActive;
        return this;
    }

    public UserDtoBuilder WithFirmId(int? firmId)
    {
        _firmId = firmId;
        return this;
    }

    public UserDto Build()
    {
        return new UserDto
        {
            Id = _id,
            Email = _email,
            FirstName = _firstName,
            LastName = _lastName,
            RoleId = _roleId,
            IsActive = _isActive,
            FirmId = _firmId
        };
    }
}