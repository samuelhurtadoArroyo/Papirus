namespace Papirus.WebApi.Api.Validators;

public class CaseValidator : AbstractValidator<CaseDto>
{
    public CaseValidator()
    {
        RuleFor(m => m.Id)
            .NotNull();

        RuleFor(m => m.GuidIdentifier)
            .NotNull()
            .Must(GuidValidator.IsValidGuid);

        RuleFor(m => m.Court)
            .NotEmpty()
            .MaximumLength(ValidationConst.MaxFieldLength128)
            .WithName("Corte");

        RuleFor(m => m.City)
             .NotEmpty()
             .MaximumLength(ValidationConst.MaxFieldLength)
             .WithName("Ciudad");

#pragma warning disable S125 // Sections of code should not be commented out

        //RuleFor(m => m.Amount)
        //    .NotEmpty()
        //    .GreaterThan(0)
        //    .WithName("Monto");

        //RuleFor(m => m.ProcessTypeId)
        //    .NotEmpty()
        //    .IsInEnum()
        //    .WithName("Tipo de Proceso");

        //RuleFor(m => m.ProcessId)
        //    .NotEmpty()
        //    .WithName("Identificador de Proceso");
#pragma warning restore S125 // Sections of code should not be commented out

        RuleFor(m => m.FilePath)
            .NotEmpty()
            .WithName("Ruta de Archivo");

        RuleFor(m => m.FileName)
            .NotEmpty()
            .WithName("Nombre de Archivo");
    }
}