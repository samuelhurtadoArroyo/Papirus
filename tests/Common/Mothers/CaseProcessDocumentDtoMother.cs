namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class CaseProcessDocumentDtoMother
{
    public static CaseProcessDocumentDto Create(int id, int documentTypeId, int processDocumentTypeId, int caseId,
        string fileName, string filePath)
    {
        return new CaseProcessDocumentDtoBuilder()
               .WithId(id)
               .WithDocumentTypeId(documentTypeId)
               .WithProcessDocumentTypeId(processDocumentTypeId)
               .WithCaseId(caseId)
               .WithFileName(fileName)
               .WithFilePath(filePath)
               .Build();
    }

    public static CaseProcessDocumentDto DemandCaseProcessDocument()
    {
        return Create(id: 1, documentTypeId: 1, processDocumentTypeId: 1, caseId: 1, fileName: "autoadmite1234", filePath: "");
    }

    public static List<CaseProcessDocumentDto> GetCaseProcessDocumentList()
    {
        return [
            DemandCaseProcessDocument()
        ];
    }

    public static List<CaseProcessDocumentDto> GetCaseProcessDocumentList(int quantity)
    {
        var caseProcessDocumentFaker = new Faker<CaseProcessDocumentDto>()
            .RuleFor(o => o.Id, f => f.IndexVariable)
            .RuleFor(o => o.DocumentTypeId, f => f.IndexVariable)
            .RuleFor(o => o.ProcessDocumentTypeId, f => f.IndexVariable)
            .RuleFor(o => o.CaseId, f => f.IndexVariable++)
            .RuleFor(o => o.FilePath, f => $"Name{f.IndexFaker}")
            .RuleFor(o => o.FileName, f => $"FilePath{f.IndexFaker}.pdf");

        return caseProcessDocumentFaker.Generate(quantity);
    }
}