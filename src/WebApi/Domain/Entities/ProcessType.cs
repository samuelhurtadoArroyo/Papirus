namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class ProcessType : EntityBase
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Case> Cases { get; set; } = [];

    public virtual ICollection<ProcessTemplate> ProcessTemplates { get; set; } = [];

    public virtual ICollection<Process> Processes { get; set; } = [];
}