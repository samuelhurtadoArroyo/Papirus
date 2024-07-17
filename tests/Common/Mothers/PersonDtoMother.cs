using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class PersonDtoMother
{
    public static PersonDto Create(int id, Guid guidIdentifier, PersonTypeId? PersonDtoTypeId, string? name, IdentificationTypeId? identificationTypeId, string? identificationNumber, string? supporFileName, string? supportFilePath)
    {
        return new PersonDtoBuilder()
               .WithId(id)
               .WithGuidIdentifier(guidIdentifier)
               .WithPersonTypeId(PersonDtoTypeId)
               .WithName(name)
               .WithIdentificationTypeId(identificationTypeId)
               .WithIdentificationNumber(identificationNumber)
               .WithSupportFileName(supporFileName)
               .WithSupportFilePath(supportFilePath)
               .Build();
    }

    public static PersonDto NaturalCCPerson()
    {
        return Create(1, Guid.Parse("5616dfb8-b212-4bae-b785-4f8b7d6eda8c"), PersonTypeId.Natural, "Natural Person CC", IdentificationTypeId.CitizenshipId, "123", "naturalDocument.pdf", "/");
    }

    public static PersonDto NaturalCEPerson()
    {
        return Create(2, Guid.Parse("440e85d0-6638-45f5-9805-51214be0df6e"), PersonTypeId.Natural, "Natural Person CE", IdentificationTypeId.ForeignCitizenshipId, "123", "naturalDocument.pdf", "/");
    }

    public static PersonDto NaturalPTPerson()
    {
        return Create(3, Guid.Parse("b36d6ef1-6001-4958-bac6-edfed93bba49"), PersonTypeId.Natural, "Natural Person PT", IdentificationTypeId.Passport, "123", "naturalDocument.pdf", "/");
    }

    public static PersonDto NaturalNITPerson()
    {
        return Create(4, Guid.Parse("a6a74cc9-b2a3-4150-bbff-aa5d8514244c"), PersonTypeId.Natural, "Natural Person NIT", IdentificationTypeId.TaxIdentificationNumber, "123", "naturalDocument.pdf", "/");
    }

    public static PersonDto NaturalRUTPerson()
    {
        return Create(5, Guid.Parse("70362972-c201-416d-83ad-58d16b5b13c8"), PersonTypeId.Natural, "Natural Person RUT", IdentificationTypeId.UniqueTaxRegistryNumber, "123", "naturalDocument.pdf", "/");
    }

    public static PersonDto LegalCCPerson()
    {
        return Create(6, Guid.Parse("4e21167f-c9bf-463b-baf0-db7d4402d188"), PersonTypeId.Legal, "Legal Person CC", IdentificationTypeId.CitizenshipId, "123", "naturalDocument.pdf", "/");
    }

    public static PersonDto LegalCEPerson()
    {
        return Create(7, Guid.Parse("642e628d-3c8f-448f-ab22-b8b946d24e22"), PersonTypeId.Legal, "Legal Person CE", IdentificationTypeId.ForeignCitizenshipId, "123", "naturalDocument.pdf", "/");
    }

    public static PersonDto LegalPTPerson()
    {
        return Create(8, Guid.Parse("5947bfaf-fe76-44e5-9a43-9ef4ce98127d"), PersonTypeId.Natural, "Legal Person PT", IdentificationTypeId.Passport, "123", "naturalDocument.pdf", "/");
    }

    public static PersonDto LegalNITPerson()
    {
        return Create(9, Guid.Parse("f9e54c41-b08f-42b5-b51f-83b263ba13bd"), PersonTypeId.Natural, "Legal Person NIT", IdentificationTypeId.TaxIdentificationNumber, "123", "naturalDocument.pdf", "/");
    }

    public static PersonDto LegalRUTPerson()
    {
        return Create(10, Guid.Parse("343b51e0-2c2d-4918-942c-0737d8ef99d0"), PersonTypeId.Natural, "Legal Person RUT", IdentificationTypeId.UniqueTaxRegistryNumber, "123", "naturalDocument.pdf", "/");
    }

    public static PersonDto NoConfigPerson()
    {
        return Create(12, Guid.Parse("395594d0-3fe8-4d01-b036-080cad0cc265"), PersonTypeId.Natural, "No Config", null, null, null, null);
    }

    public static PersonDto GetEmptyPerson()
    {
        return Create(1, Guid.Parse("664f424d-7575-4250-9fcb-e230c33038da"), null!, null, null, null, null, null);
    }

    public static PersonDto GetPersonWithMaxLengths()
    {
        var maxField = "A".PadRight(ValidationConst.MaxFieldLength + 1, 'A');
        var maxName = "A".PadRight(ValidationConst.MaxFieldLongLength + 1, 'A');

        return Create(1, Guid.Parse("f6333ef9-2079-4891-9253-f8958f5f15f1"), null, maxName, null, maxField, null, null);
    }

    public static PersonDto GetPersonWithInvalidEmail()
    {
        return Create(1, Guid.Parse("af93ff8c-ea1e-48bd-94bc-baa1920b13e3"), PersonTypeId.Natural, "Person", IdentificationTypeId.CitizenshipId, "123", "fileName.pdf", "");
    }

    public static List<PersonDto> GetPersonList()
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
}