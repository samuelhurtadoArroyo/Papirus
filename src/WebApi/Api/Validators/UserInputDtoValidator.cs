namespace Papirus.WebApi.Api.Validators;

public class UserInputDtoValidator : AbstractValidator<UserInputDto>
{
    public UserInputDtoValidator()
    {
        RuleFor(m => m.FirstName)
            .NotEmpty()
            .MaximumLength(ValidationConst.MaxFieldLength)
            .WithName("Nombre");

        RuleFor(m => m.LastName)
            .NotEmpty()
            .MaximumLength(ValidationConst.MaxFieldLength)
            .WithName("Apellido");

        RuleFor(m => m.Email)
            .NotEmpty()
            .EmailAddress()
            .WithName("Correo Electrónico")
            .MaximumLength(ValidationConst.MaxEmailLength);

        RuleFor(m => m.Password).SetValidator(new PasswordValidator());
    }
}