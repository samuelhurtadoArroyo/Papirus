namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class UpdateCaseDocumentFieldValueDtoMother
{
    public static UpdateCaseDocumentFieldValueDto Create(int id, string name)
    {
        return new UpdateCaseDocumentFieldValueDtoBuilder()
            .WithId(id)
            .WithValue(name)
            .Build();
    }

    public static UpdateCaseDocumentFieldValueDto GetEmptyValue(int id)
    {
        return new UpdateCaseDocumentFieldValueDtoBuilder()
            .WithId(id)
            .Build();
    }
}