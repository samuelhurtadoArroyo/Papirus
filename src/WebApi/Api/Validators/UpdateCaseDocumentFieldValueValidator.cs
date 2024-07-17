namespace Papirus.WebApi.Api.Validators;

public class UpdateCaseDocumentFieldValueValidator : AbstractValidator<UpdateCaseDocumentFieldValueDto>
{
    public UpdateCaseDocumentFieldValueValidator()
    {
        RuleFor(m => m.Id)
            .NotNull();

        RuleFor(m => m.FieldValue)
            .NotEmpty()
            .WithName("Field Value");
    }
}