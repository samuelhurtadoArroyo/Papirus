namespace Papirus.WebApi.Api.Validators;

public class ActorValidator : AbstractValidator<ActorDto>
{
    public ActorValidator()
    {
        RuleFor(m => m.Id)
            .NotNull();

        RuleFor(m => m.ActorTypeId)
            .NotEmpty()
            .IsInEnum()
            .WithName("Tipo de Actor")
            ;

        RuleFor(m => m.PersonId)
            .NotEmpty()
            .WithName("Persona");

        RuleFor(m => m.CaseId)
            .NotEmpty()
            .WithName("Caso");
    }
}