namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class CaseProcessDocument : EntityBase
{
    public int DocumentTypeId { get; set; }

    public int? ProcessDocumentTypeId { get; set; }

    public int CaseId { get; set; }

    public string FileName { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public virtual Case Case { get; set; } = null!;

    public virtual ICollection<CaseDocumentFieldValue> CaseDocumentFieldValues { get; set; } = [];

    public virtual DocumentType DocumentType { get; set; } = null!;

    public virtual ProcessDocumentType ProcessDocumentType { get; set; } = null!;
}