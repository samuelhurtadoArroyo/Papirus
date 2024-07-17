namespace Papirus.WebApi.Api.Validators;

public class PermissionValidator : AbstractValidator<PermissionDto>
{
    public PermissionValidator()
    {
        RuleFor(m => m.Id)
            .NotNull();

        RuleFor(m => m.Name)
            .NotEmpty()
            .MaximumLength(ValidationConst.MaxFieldLength)
            .WithName("Nombre");

        RuleFor(m => m.Description)
            .NotEmpty()
            .MaximumLength(ValidationConst.MaxFieldLongLength)
            .WithName("Descripción");
    }
}