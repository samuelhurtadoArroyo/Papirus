namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class UpdateCaseDocumentFieldValueDtoBuilder
{
    private int _id;

    private string _value;

    public UpdateCaseDocumentFieldValueDtoBuilder()
    {
        _id = 0;
        _value = string.Empty;
    }

    public UpdateCaseDocumentFieldValueDtoBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public UpdateCaseDocumentFieldValueDtoBuilder WithValue(string name)
    {
        _value = name;
        return this;
    }

    public UpdateCaseDocumentFieldValueDto Build()
    {
        return new UpdateCaseDocumentFieldValueDto
        {
            Id = _id,
            FieldValue = _value
        };
    }
}