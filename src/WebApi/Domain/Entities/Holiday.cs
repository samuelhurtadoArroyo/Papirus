namespace Papirus.WebApi.Domain.Entities;

[ExcludeFromCodeCoverage]
public class Holiday : EntityBase
{
    public DateTime Date { get; set; }

    public string Description { get; set; } = null!;
}