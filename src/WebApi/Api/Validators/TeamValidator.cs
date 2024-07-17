namespace Papirus.WebApi.Api.Validators;

public class TeamValidator : AbstractValidator<TeamDto>
{
    public TeamValidator()
    {
        RuleFor(m => m.Id)
            .NotNull();

        RuleFor(m => m.Name)
            .NotEmpty()
            .MaximumLength(ValidationConst.MaxFieldLength)
            .WithName("Nombre");
    }
}