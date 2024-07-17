namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class DocumentType : EntityBase
{
    public string Name { get; set; } = null!;

    public virtual ICollection<CaseDocumentFieldValue> CaseDocumentFieldValues { get; set; } = [];

    public virtual ICollection<CaseProcessDocument> CaseProcessDocuments { get; set; } = [];

    public virtual ICollection<ProcessDocumentType> ProcessDocumentTypes { get; set; } = [];
}