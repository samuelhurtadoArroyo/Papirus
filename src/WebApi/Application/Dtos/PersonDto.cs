using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.WebApi.Api.Dtos;

/// <summary>
/// Represents a person with identification and contact information.
/// </summary>
public class PersonDto
{
    /// <summary>
    /// The unique identifier for the person.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// A GUID representing the person for system-wide identification.
    /// </summary>
    public Guid GuidIdentifier { get; set; }

    /// <summary>
    /// The type of person (Natural/Legal)
    /// </summary>
    public PersonTypeId? PersonTypeId { get; set; }

    /// <summary>
    /// The name of the person.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The type of identification document.
    /// </summary>
    public IdentificationTypeId? IdentificationTypeId { get; set; }

    /// <summary>
    /// The identification number of the document.
    /// </summary>
    public string? IdentificationNumber { get; set; }

    /// <summary>
    /// The name of the support file, if any.
    /// </summary>
    public string? SupportFileName { get; set; }

    /// <summary>
    /// The path to the support file, if any.
    /// </summary>
    public string? SupportFilePath { get; set; }
}