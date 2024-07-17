namespace Papirus.WebApi.Api.Validators;

public class LoginInputDtoValidator : AbstractValidator<LoginInputDto>
{
    public LoginInputDtoValidator()
    {
        RuleFor(m => m.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(ValidationConst.MaxEmailLength)
            .WithName("Correo Electrónico");

        RuleFor(m => m.Password)
            .NotEmpty()
            .MaximumLength(ValidationConst.MaxFieldLength)
            .WithName("Contraseña");
    }
}