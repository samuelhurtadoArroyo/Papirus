namespace Papirus.WebApi.Domain.Dtos;

[ExcludeFromCodeCoverage]
public class QueryRequest
{
    public int? PageNumber { get; set; } = null;

    public int? PageSize { get; set; } = null;

    public string? SearchString { get; set; } = null;

    public IEnumerable<FilterParams>? FilterParams { get; set; }

    public IEnumerable<SortingParams>? SortingParams { set; get; }
}