namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class CaseDocumentFieldValueMother
{
    public static CaseDocumentFieldValue Create(int id, int caseId, int? documentTypeId, string fieldValue)
    {
        return new CaseDocumentFieldValueBuilder()
               .WithId(id)
               .WithCaseId(caseId)
               .WithDocumentTypeId(documentTypeId)
               .WithFieldValue(fieldValue)
               .Build();
    }

    public static CaseDocumentFieldValue DefaultFieldValue()
    {
        return Create(1, 1, 1, "Sample Field Value");
    }

    public static CaseDocumentFieldValue AnotherFieldValue()
    {
        return Create(2, 1, 2, "Another Field Value");
    }

    public static List<CaseDocumentFieldValue> GetFieldValueList()
    {
        return
            [
                DefaultFieldValue(),
                AnotherFieldValue()
            ];
    }

    public static List<CaseDocumentFieldValue> GetFieldValueList(int quantity)
    {
        var fieldValueList = new List<CaseDocumentFieldValue>
            {
                Create(0, 0, 0, "")
            };

        var fieldValueFaker = new Faker<CaseDocumentFieldValue>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.CaseId, f => f.Random.Int(1, 100))
            .RuleFor(o => o.DocumentTypeId, f => f.Random.Int(1, 5))
            .RuleFor(o => o.FieldValue, f => f.Lorem.Sentence());

        fieldValueList.AddRange(fieldValueFaker.Generate(quantity));

        return fieldValueList;
    }

    public static List<CaseDocumentFieldValue> GetRandomFieldValueList(int quantity)
    {
        var fieldValueFaker = new Faker<CaseDocumentFieldValue>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.CaseId, f => f.Random.Int(1, 100))
            .RuleFor(o => o.DocumentTypeId, f => f.Random.Int(1, 5))
            .RuleFor(o => o.FieldValue, f => f.Lorem.Sentence());

        return fieldValueFaker.Generate(quantity);
    }
}