using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class PersonDtoBuilder
{
    private int _id;

    private Guid _guidIdentifier;

    private PersonTypeId? _personTypeId;

    private string? _name;

    private string? _email;

    private IdentificationTypeId? _identificationTypeId;

    private string? _identificationNumber;

    private string? _supportFileName;

    private string? _supportFilePath;

    public PersonDtoBuilder()
    {
        _id = 0;
        _guidIdentifier = Guid.NewGuid();
        _personTypeId = null!;
        _name = null!;
        _email = null!;
        _identificationTypeId = null!;
        _identificationNumber = null!;
        _supportFileName = null!;
        _supportFilePath = null!;
    }

    public PersonDtoBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public PersonDtoBuilder WithGuidIdentifier(Guid guidIdentifier)
    {
        _guidIdentifier = guidIdentifier;
        return this;
    }

    public PersonDtoBuilder WithPersonTypeId(PersonTypeId? personTypeId)
    {
        _personTypeId = personTypeId;
        return this;
    }

    public PersonDtoBuilder WithName(string? name)
    {
        _name = name;
        return this;
    }

    public PersonDtoBuilder WithEmail(string? email)
    {
        _email = email;
        return this;
    }

    public PersonDtoBuilder WithIdentificationTypeId(IdentificationTypeId? identificationTypeId)
    {
        _identificationTypeId = identificationTypeId;
        return this;
    }

    public PersonDtoBuilder WithIdentificationNumber(string? identificationNumber)
    {
        _identificationNumber = identificationNumber;
        return this;
    }

    public PersonDtoBuilder WithSupportFileName(string? supportFileName)
    {
        _supportFileName = supportFileName;
        return this;
    }

    public PersonDtoBuilder WithSupportFilePath(string? supportFilePath)
    {
        _supportFilePath = supportFilePath;
        return this;
    }

    public PersonDto Build()
    {
        return new PersonDto
        {
            Id = _id,
            GuidIdentifier = _guidIdentifier,
            PersonTypeId = _personTypeId,
            Name = _name,
            IdentificationTypeId = _identificationTypeId,
            IdentificationNumber = _identificationNumber,
            SupportFileName = _supportFileName,
            SupportFilePath = _supportFilePath
        };
    }
}