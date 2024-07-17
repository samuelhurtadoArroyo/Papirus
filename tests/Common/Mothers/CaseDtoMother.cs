using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class CaseDtoMother
{
    public static CaseDto Create(int id, Guid guidIdentifier, DateTime? registrationDate, string? court, string? city, decimal? amount, DateTime? submissionDate, string? submissionIdentifier, DateTime? deadLineDate
        , ProcessTypeId processTypeId, int? processId, int? subProcessId, string? filePath, string? fileName, bool? isAssigned, bool? isAnswered, DateTime? AnsweredDate)
    {
        return new CaseDtoBuilder()
               .WithId(id)
               .WithGuidIdentifier(guidIdentifier)
               .WithRegistrationDate(registrationDate)
               .WithCourt(court)
               .WithCity(city)
               .WithAmount(amount)
               .WithSubmissionDate(submissionDate)
               .WithSubmissionIdentifier(submissionIdentifier)
               .WithDeadLineDate(deadLineDate)
               .WithProcessTypeId(processTypeId)
               .WithProcessId(processId)
               .WithSubProcessId(subProcessId)
               .WithFilePath(filePath)
               .WithFileName(fileName)
               .WithIsAssigned(isAssigned)
               .WithIsAnswered(isAnswered)
               .WithAnswereDate(AnsweredDate)
               .Build();
    }

    public static CaseDto DemandCase()
    {
        return Create(1, Guid.Parse("8b2acf7e-3ec5-44ce-9af6-5c8fbd429c13"), new DateTime(2024, 3, 1, 18, 10, 0, DateTimeKind.Utc), "court", "city", 1000, null, null, null, ProcessTypeId.Demand, 1, null, "", "case1.pdf", true, null, null);
    }

    public static CaseDto GuardianshipCase()
    {
        return Create(1, Guid.Parse("71959417-004e-44b0-8df6-e40085447cb3"), new DateTime(2024, 3, 1, 18, 10, 0, DateTimeKind.Utc), "court", "city", 1000, null, null, null, ProcessTypeId.Guardianship, 1, null, "", "case1.pdf", true, null, null);
    }

    public static CaseDto GetEmptyCase()
    {
        return Create(1, Guid.Empty, null, null, null, null, null, null, null, ProcessTypeId.Demand, null, null, null, null, null, null, null);
    }

    public static CaseDto GetCaseWithMaxLengths()
    {
        var maxField = "A".PadRight(ValidationConst.MaxFieldLength128 + 1, 'A');

        return Create(1, Guid.Parse("94552511-e635-4f4d-8579-f911178f29e9"), new DateTime(2024, 3, 1, 18, 10, 0, DateTimeKind.Utc), maxField, maxField, 100, null, maxField, null, ProcessTypeId.Guardianship, 1, null, "/", "case1.pdf", true, null, null);
    }

    public static CaseDto IsDocumentAnswered()
    {
        return Create(1, Guid.Parse("8b2acf7e-3ec5-44ce-9af6-5c8fbd429c13"), new DateTime(2024, 3, 1, 18, 10, 0, DateTimeKind.Utc), "court", "city", 1000, null, null, null, ProcessTypeId.Demand, 1, null, "/", "case1.pdf", true, true, DateTime.Now);
    }

    public static List<CaseDto> GetCaseList()
    {
        return [
            DemandCase(),
            GuardianshipCase()
        ];
    }
}