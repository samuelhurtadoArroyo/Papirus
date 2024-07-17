using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class CaseMother
{
    public static Case Create(int id, Guid guidIdentifier, DateTime? registrationDate, string court, string city, decimal amount,
        DateTime? submissionDate, string? submissionIdentifier, DateTime? deadLineDate,
        ProcessTypeId processTypeId, string? emailHtmlBody, int processId, int? subProcessId, string filePath, string fileName, bool isAssigned, bool? isAnswered, DateTime? answeredDate)
    {
        return new CaseBuilder()
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
               .WithEmailHtmlBody(emailHtmlBody)
               .WithProcessId(processId)
               .WithSubProcessId(subProcessId)
               .WithFilePath(filePath)
               .WithFileName(fileName)
               .WithIsAssigned(isAssigned)
               .WithIsAnswered(isAnswered)
               .WithAnswereDate(answeredDate)
               .Build();
    }

    public static Case DemandCase()
    {
        return Create(1, Guid.Parse("8b2acf7e-3ec5-44ce-9af6-5c8fbd429c13"), new DateTime(2024, 3, 1, 18, 10, 0, DateTimeKind.Utc), "court", "city", 1000, null, null, null, ProcessTypeId.Demand, null, 1, null, "", "case1.pdf", true, null, null);
    }

    public static Case GuardianshipCase()
    {
        return Create(1, Guid.Parse("71959417-004e-44b0-8df6-e40085447cb3"), new DateTime(2024, 3, 1, 18, 10, 0, DateTimeKind.Utc), "court", "city", 1000, null, null, null, ProcessTypeId.Guardianship, null, 1, null, "", "case1.pdf", true, null, null);
    }

    public static List<Case> GetCaseList()
    {
        return
        [
            DemandCase(),
            GuardianshipCase()
        ];
    }

    public static List<Case> GetCaseList(int quantity)
    {
        var caseList = new List<Case>()
        {
            Create(0,
                    Guid.Empty,
                    new DateTime(2024, 3, 1, 18, 10, 0, DateTimeKind.Utc),
                    "",
                    "",
                    0,
                    null,
                    null,
                    null,
                    ProcessTypeId.Guardianship,
                    "",
                    1,
                    null,
                    "",
                    "",
                    true,
                    null,
                    null)
        };

        var caseFaker = new Faker<Case>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.RegistrationDate, f => f.Date.Recent())
            .RuleFor(o => o.Court, f => $"Court{f.IndexFaker}")
            .RuleFor(o => o.City, f => $"City{f.IndexFaker}")
            .RuleFor(o => o.Amount, _ => 0)
            .RuleFor(o => o.SubmissionDate, f => f.Date.Recent())
            .RuleFor(o => o.ProcessTypeId, _ => 1)
            .RuleFor(o => o.SubProcessId, _ => null)
            .RuleFor(o => o.FilePath, f => $"Name{f.IndexFaker}")
            .RuleFor(o => o.FileName, f => $"FilePath{f.IndexFaker}.pdf");

        caseList.AddRange(caseFaker.Generate(quantity));

        return caseList;
    }

    public static List<Case> GetRandomPersonList(int quantity)
    {
        var CaseFaker = new Faker<Case>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.RegistrationDate, f => f.Date.Between(new DateTime(2024, 1, 1, 18, 10, 0, DateTimeKind.Utc), new DateTime(2024, 6, 1, 18, 10, 0, DateTimeKind.Utc)))
            .RuleFor(o => o.Court, f => $"Court{f.IndexFaker}")
            .RuleFor(o => o.City, f => $"City{f.IndexFaker}")
            .RuleFor(o => o.Amount, f => f.Random.Decimal(0, 20000))
            .RuleFor(o => o.SubmissionDate, f => f.Date.Between(new DateTime(2024, 1, 1, 18, 10, 0, DateTimeKind.Utc), new DateTime(2024, 6, 1, 18, 10, 0, DateTimeKind.Utc)))
            .RuleFor(o => o.ProcessTypeId, f => f.Random.Int(1, 2))
            .RuleFor(o => o.SubProcessId, _ => null)
            .RuleFor(o => o.FilePath, f => $"Name{f.IndexFaker}")
            .RuleFor(o => o.FileName, f => $"FilePath{f.IndexFaker}.pdf");

        return CaseFaker.Generate(quantity);
    }
}