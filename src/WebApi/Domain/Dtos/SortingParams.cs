namespace Papirus.WebApi.Domain.Dtos;

[ExcludeFromCodeCoverage]
public class SortingParams
{
    public SortOrders SortOrder { get; set; } = SortOrders.Asc;

    public string ColumnName { get; set; } = null!;
}