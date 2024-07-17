namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class LoginInputDtoBuilder
{
    private string _email;

    private string _password;

    public LoginInputDtoBuilder()
    {
        _email = string.Empty;
        _password = string.Empty;
    }

    public LoginInputDtoBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public LoginInputDtoBuilder WithPassword(string password)
    {
        _password = password;
        return this;
    }

    public LoginInputDto Build()
    {
        return new LoginInputDto
        {
            Email = _email,
            Password = _password
        };
    }
}