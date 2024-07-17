namespace Papirus.WebApi.Api.Validators;

public class PersonValidator : AbstractValidator<PersonDto>
{
    public PersonValidator()
    {
        RuleFor(m => m.Id)
            .NotNull();

        RuleFor(m => m.GuidIdentifier)
            .NotNull()
            .Must(GuidValidator.IsValidGuid);

        RuleFor(m => m.PersonTypeId)
            .NotEmpty()
            .IsInEnum()
            .WithName("Tipo de Persona");

        RuleFor(m => m.Name)
             .NotEmpty()
             .MaximumLength(ValidationConst.MaxFieldLongLength)
             .WithName("Nombre");

        RuleFor(m => m.IdentificationTypeId)
            .NotEmpty()
            .IsInEnum()
            .WithName("Tipo de Identificación");

        RuleFor(m => m.IdentificationNumber)
            .NotEmpty()
            .MaximumLength(ValidationConst.MaxFieldLength)
            .WithName("Número de Identificación");
    }
}