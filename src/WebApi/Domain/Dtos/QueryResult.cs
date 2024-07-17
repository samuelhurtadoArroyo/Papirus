namespace Papirus.WebApi.Domain.Dtos;

[ExcludeFromCodeCoverage]
public class QueryResult<TModel>
{
    public List<TModel> Items { get; set; } = null!;

    public PaginationData PaginationData { get; set; } = new PaginationData();
}