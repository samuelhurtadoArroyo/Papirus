namespace Papirus.WebApi.Api.Validators;

public class RoleValidator : AbstractValidator<RoleDto>
{
    public RoleValidator()
    {
        RuleFor(m => m.Id)
            .NotNull();

        RuleFor(m => m.Name)
            .NotEmpty()
            .MaximumLength(ValidationConst.MaxFieldLength)
            .WithName("Nombre");
    }
}