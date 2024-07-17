namespace Papirus.WebApi.Api.Validators;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(password => password)
            .NotEmpty()
            .WithMessage("'Contraseña' no debería estar vacío.");

        RuleFor(password => password)
            .MaximumLength(ValidationConst.MaxPasswordLength)
            .WithMessage(password => $"'Contraseña' debe ser menor o igual que {ValidationConst.MaxPasswordLength} caracteres. Ingresó {password.Length} caracteres.");

        RuleFor(password => password)
            .MinimumLength(ValidationConst.MinPasswordLength)
            .WithMessage(password => $"'Contraseña' debe tener al menos {ValidationConst.MinPasswordLength} caracteres. Ingresó {password.Length} caracteres.");

        RuleFor(password => password)
            .Matches("[A-Z]").When(password => ValidationConst.MinUppercaseLetters > 0)
            .WithMessage($"'Contraseña' debe contener al menos {ValidationConst.MinUppercaseLetters} letra(s) mayúscula(s).");

        RuleFor(password => password)
            .Matches("[a-z]").When(password => ValidationConst.MinLowercaseLetters > 0)
            .WithMessage($"'Contraseña' debe contener al menos {ValidationConst.MinLowercaseLetters} letra(s) minúscula(s).");

        RuleFor(password => password)
            .Matches("[0-9]").When(password => ValidationConst.MinDigits > 0)
            .WithMessage($"'Contraseña' debe contener al menos {ValidationConst.MinDigits} dígito(s).");

        RuleFor(password => password)
            .Matches("[^a-zA-Z0-9]").When(password => ValidationConst.MinSpecialCharacters > 0)
            .WithMessage($"'Contraseña' debe contener al menos {ValidationConst.MinSpecialCharacters} carácter(es) especial(es).");
    }
}