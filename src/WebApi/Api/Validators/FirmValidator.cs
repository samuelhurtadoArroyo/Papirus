namespace Papirus.WebApi.Api.Validators;

public class FirmValidator : AbstractValidator<FirmDto>
{
    public FirmValidator()
    {
        RuleFor(m => m.Id)
            .NotNull();

        RuleFor(m => m.Name)
            .NotEmpty()
            .MaximumLength(ValidationConst.MaxFieldLength)
            .WithName("Nombre");

        RuleFor(m => m.GuidIdentifier)
            .Must(GuidValidator.IsValidGuid)
            .WithName("Identificador GUID");
    }
}