namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class SubProcess : EntityBase
{
    public string Description { get; set; } = null!;

    public string? Abbreviation { get; set; }

    public int ProcessId { get; set; }

    public virtual ICollection<Case> Cases { get; set; } = [];

    public virtual Process Process { get; set; } = null!;

    public virtual ICollection<ProcessTemplate> ProcessTemplates { get; set; } = [];
}