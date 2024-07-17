namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class QueryResultBuilder<TModel>
{
    private List<TModel> _items;

    private PaginationData _paginationData;

    public QueryResultBuilder()
    {
        _items = null!;
        _paginationData = null!;
    }

    public QueryResultBuilder<TModel> WithItems(List<TModel> items)
    {
        _items = items;
        return this;
    }

    public QueryResultBuilder<TModel> WithPaginationData(PaginationData paginationData)
    {
        _paginationData = paginationData;
        return this;
    }

    public QueryResult<TModel> Build()
    {
        return new QueryResult<TModel>
        {
            Items = _items,
            PaginationData = _paginationData
        };
    }
}