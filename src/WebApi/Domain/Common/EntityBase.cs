namespace Papirus.WebApi.Domain.Common;

[ExcludeFromCodeCoverage]
public abstract class EntityBase
{
    [Key]
    [Required(ErrorMessage = "The Id is required")]
    public int Id { get; set; }
}