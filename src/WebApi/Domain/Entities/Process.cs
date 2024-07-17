namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class Process : EntityBase
{
    public string Name { get; set; } = null!;

    public int ProcessTypeId { get; set; }

    public virtual ICollection<Case> Cases { get; set; } = [];

    public virtual ICollection<ProcessDocumentType> ProcessDocumentTypes { get; set; } = [];

    public virtual ICollection<ProcessTemplate> ProcessTemplates { get; set; } = [];

    public virtual ProcessType ProcessType { get; set; } = null!;

    public virtual ICollection<SubProcess> SubProcesses { get; set; } = [];
}