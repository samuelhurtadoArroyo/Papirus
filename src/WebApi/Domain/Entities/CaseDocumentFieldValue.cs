namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class CaseDocumentFieldValue : EntityBase
{
    public int CaseProcessDocumentId { get; set; }

    public int DocumentTypeId { get; set; }

    public int? ProcessDocumentTypeId { get; set; }

    public int CaseId { get; set; }

    public string Name { get; set; } = null!;

    public string Tag { get; set; } = null!;

    public int? Multiplicity { get; set; }

    public string FieldValue { get; set; } = null!;

    public virtual CaseProcessDocument CaseProcessDocument { get; set; } = null!;

    public virtual DocumentType DocumentType { get; set; } = null!;

    public virtual ProcessDocumentType ProcessDocumentType { get; set; } = null!;
}