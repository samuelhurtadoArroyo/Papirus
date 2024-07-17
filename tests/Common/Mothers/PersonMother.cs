using Papirus.WebApi.Domain.Define.Enums;
using Person = Papirus.WebApi.Domain.Entities.Person;

namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class PersonMother
{
    public static Person Create(int id, Guid guidIdentifier, PersonTypeId personTypeId, string name, string email, IdentificationTypeId identificationTypeId, string identificationNumber, string supporFileName, string supportFilePath)
    {
        return new PersonBuilder()
               .WithId(id)
               .WithGuidIdentifier(guidIdentifier)
               .WithPersonTypeId(personTypeId)
               .WithName(name)
               .WithIdentificationTypeId(identificationTypeId)
               .WithIdentificationNumber(identificationNumber)
               .WithSupportFileName(supporFileName)
               .WithSupportFilePath(supportFilePath)
               .Build();
    }

    public static Person NaturalCCPerson()
    {
        return Create(1, Guid.Parse("5616dfb8-b212-4bae-b785-4f8b7d6eda8c"), PersonTypeId.Natural, "Natural Person CC", "naturalPersonCC@papirus.com", IdentificationTypeId.CitizenshipId, "123", "naturalDocument.pdf", "/");
    }

    public static Person NaturalCEPerson()
    {
        return Create(2, Guid.Parse("440e85d0-6638-45f5-9805-51214be0df6e"), PersonTypeId.Natural, "Natural Person CE", "naturalPersonCE@papirus.com", IdentificationTypeId.ForeignCitizenshipId, "123", "naturalDocument.pdf", "/");
    }

    public static Person NaturalPTPerson()
    {
        return Create(3, Guid.Parse("b36d6ef1-6001-4958-bac6-edfed93bba49"), PersonTypeId.Natural, "Natural Person PT", "naturalPersonPT@papirus.com", IdentificationTypeId.Passport, "123", "naturalDocument.pdf", "/");
    }

    public static Person NaturalNITPerson()
    {
        return Create(4, Guid.Parse("a6a74cc9-b2a3-4150-bbff-aa5d8514244c"), PersonTypeId.Natural, "Natural Person NIT", "naturalPersonNIT@papirus.com", IdentificationTypeId.TaxIdentificationNumber, "123", "naturalDocument.pdf", "/");
    }

    public static Person NaturalRUTPerson()
    {
        return Create(5, Guid.Parse("70362972-c201-416d-83ad-58d16b5b13c8"), PersonTypeId.Natural, "Natural Person RUT", "naturalPersonRUT@papirus.com", IdentificationTypeId.UniqueTaxRegistryNumber, "123", "naturalDocument.pdf", "/");
    }

    public static Person LegalCCPerson()
    {
        return Create(6, Guid.Parse("4e21167f-c9bf-463b-baf0-db7d4402d188"), PersonTypeId.Legal, "Legal Person CC", "legalPersonCC@papirus.com", IdentificationTypeId.CitizenshipId, "123", "naturalDocument.pdf", "/");
    }

    public static Person LegalCEPerson()
    {
        return Create(7, Guid.Parse("642e628d-3c8f-448f-ab22-b8b946d24e22"), PersonTypeId.Legal, "Legal Person CE", "legalPersonCE@papirus.com", IdentificationTypeId.ForeignCitizenshipId, "123", "naturalDocument.pdf", "/");
    }

    public static Person LegalPTPerson()
    {
        return Create(8, Guid.Parse("5947bfaf-fe76-44e5-9a43-9ef4ce98127d"), PersonTypeId.Natural, "Legal Person PT", "legalPersonPT@papirus.com", IdentificationTypeId.Passport, "123", "naturalDocument.pdf", "/");
    }

    public static Person LegalNITPerson()
    {
        return Create(9, Guid.Parse("f9e54c41-b08f-42b5-b51f-83b263ba13bd"), PersonTypeId.Natural, "Legal Person NIT", "legalPersonNIT@papirus.com", IdentificationTypeId.TaxIdentificationNumber, "123", "naturalDocument.pdf", "/");
    }

    public static Person LegalRUTPerson()
    {
        return Create(10, Guid.Parse("343b51e0-2c2d-4918-942c-0737d8ef99d0"), PersonTypeId.Natural, "Legal Person RUT", "legalPersonRUT@papirus.com", IdentificationTypeId.UniqueTaxRegistryNumber, "123", "naturalDocument.pdf", "/");
    }

    public static List<Person> GetPersonList()
    {
        return [
            NaturalCCPerson(),
            NaturalCEPerson(),
            NaturalPTPerson(),
            NaturalNITPerson(),
            NaturalRUTPerson(),
            LegalCCPerson(),
            LegalCEPerson(),
            LegalPTPerson(),
            LegalNITPerson(),
            LegalRUTPerson()

        ];
    }

    public static List<Person> GetPersonList(int quantity)
    {
        var personList = new List<Person>()
        {
            Create(0, Guid.Empty, 0, "username", "username@email.com", 0, "","","")
        };

        var personFaker = new Faker<Person>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.PersonTypeId, _ => 1)
            .RuleFor(o => o.Name, f => $"Name{f.IndexFaker}")
            .RuleFor(o => o.IdentificationTypeId, _ => 1)
            .RuleFor(o => o.IdentificationNumber, _ => "123")
            .RuleFor(o => o.SupportFileName, f => $"Name{f.IndexFaker}.pdf")
            .RuleFor(o => o.SupportFilePath, f => $"FilePath{f.IndexFaker}");

        personList.AddRange(personFaker.Generate(quantity));

        return personList;
    }

    public static List<Person> GetRandomPersonList(int quantity)
    {
        var PersonFaker = new Faker<Person>()
            .RuleFor(o => o.Id, f => f.IndexFaker)
            .RuleFor(o => o.PersonTypeId, f => f.Random.Int(1, 2))
            .RuleFor(o => o.Name, f => $"Name{f.IndexFaker}")
            .RuleFor(o => o.IdentificationTypeId, f => f.Random.Int(1, 5))
            .RuleFor(o => o.IdentificationNumber, _ => "123")
            .RuleFor(o => o.SupportFileName, f => $"Name{f.IndexFaker}.pdf")
            .RuleFor(o => o.SupportFilePath, f => $"FilePath{f.IndexFaker}");

        return PersonFaker.Generate(quantity);
    }
}