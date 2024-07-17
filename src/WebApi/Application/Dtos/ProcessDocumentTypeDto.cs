namespace Papirus.WebApi.Application.Dtos;

[ExcludeFromCodeCoverage]
public class ProcessDocumentTypeDto
{
    public int ProcessId { get; set; }

    public int DocumentTypeId { get; set; }

    public bool Mandatory { get; set; }

    public int DocOrder { get; set; }

    public int ProcessTemplateId { get; set; }

    public virtual ICollection<CaseDocumentFieldValue> CaseDocumentFieldValues { get; set; } = [];

    public virtual ICollection<CaseProcessDocument> CaseProcessDocuments { get; set; } = [];

    public virtual DocumentType DocumentType { get; set; } = null!;

    public virtual Process Process { get; set; } = null!;

    public virtual ProcessTemplate ProcessTemplate { get; set; } = null!;
}