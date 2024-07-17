namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class CaseDocumentFieldValueBuilder
{
    private readonly CaseDocumentFieldValue _caseDocumentFieldValue;

    public CaseDocumentFieldValueBuilder()
    {
        _caseDocumentFieldValue = new CaseDocumentFieldValue();
    }

    public CaseDocumentFieldValueBuilder WithId(int id)
    {
        _caseDocumentFieldValue.Id = id;
        return this;
    }

    public CaseDocumentFieldValueBuilder WithCaseId(int caseId)
    {
        _caseDocumentFieldValue.CaseId = caseId;
        return this;
    }

    public CaseDocumentFieldValueBuilder WithDocumentTypeId(int? documentTypeId)
    {
        _caseDocumentFieldValue.DocumentTypeId = documentTypeId ?? 0;
        return this;
    }

    public CaseDocumentFieldValueBuilder WithFieldValue(string fieldValue)
    {
        _caseDocumentFieldValue.FieldValue = fieldValue;
        return this;
    }

    public CaseDocumentFieldValue Build()
    {
        return _caseDocumentFieldValue;
    }
}