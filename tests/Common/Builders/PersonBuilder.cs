using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class PersonBuilder
{
    private int _id;

    private Guid _guidIdentifier;

    private PersonTypeId _personTypeId;

    private string _name;

    private IdentificationTypeId _identificationTypeId;

    private string _identificationNumber;

    private string _supportFileName;

    private string _supportFilePath;

    public PersonBuilder()
    {
        _id = 0;
        _guidIdentifier = Guid.NewGuid();
        _personTypeId = PersonTypeId.Natural;
        _name = null!;
        _identificationTypeId = IdentificationTypeId.CitizenshipId;
        _identificationNumber = null!;
        _supportFileName = null!;
        _supportFilePath = null!;
    }

    public PersonBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public PersonBuilder WithGuidIdentifier(Guid guidIdentifier)
    {
        _guidIdentifier = guidIdentifier;
        return this;
    }

    public PersonBuilder WithPersonTypeId(PersonTypeId personTypeId)
    {
        _personTypeId = personTypeId;
        return this;
    }

    public PersonBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public PersonBuilder WithIdentificationTypeId(IdentificationTypeId identificationTypeId)
    {
        _identificationTypeId = identificationTypeId;
        return this;
    }

    public PersonBuilder WithIdentificationNumber(string identificationNumber)
    {
        _identificationNumber = identificationNumber;
        return this;
    }

    public PersonBuilder WithSupportFileName(string supportFileName)
    {
        _supportFileName = supportFileName;
        return this;
    }

    public PersonBuilder WithSupportFilePath(string supportFilePath)
    {
        _supportFilePath = supportFilePath;
        return this;
    }

    public WebApi.Domain.Entities.Person Build()
    {
        return new WebApi.Domain.Entities.Person
        {
            Id = _id,
            GuidIdentifier = _guidIdentifier,
            PersonTypeId = (int)_personTypeId,
            Name = _name,
            IdentificationTypeId = (int)_identificationTypeId,
            IdentificationNumber = _identificationNumber,
            SupportFileName = _supportFileName,
            SupportFilePath = _supportFilePath
        };
    }
}