namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class QueryRequestBuilder
{
    private int? _pageNumber;

    private int? _pageSize;

    private IEnumerable<FilterParams>? _filterParams;

    private IEnumerable<SortingParams>? _sortingParams;

    private string? _searchString;

    public QueryRequestBuilder()
    {
        _pageNumber = null!;
        _pageSize = null!;
        _filterParams = null!;
        _sortingParams = null!;
        _searchString = null!;
    }

    public QueryRequestBuilder WithPageNumber(int? pageNumber)
    {
        _pageNumber = pageNumber;
        return this;
    }

    public QueryRequestBuilder WithPageSize(int? pageSize)
    {
        _pageSize = pageSize;
        return this;
    }

    public QueryRequestBuilder WithFilterParams(IEnumerable<FilterParams>? filterParams)
    {
        _filterParams = filterParams;
        return this;
    }

    public QueryRequestBuilder WithSortingParams(IEnumerable<SortingParams>? sortingParams)
    {
        _sortingParams = sortingParams;
        return this;
    }

    public QueryRequestBuilder WithSearchString(string? searchString)
    {
        _searchString = searchString;
        return this;
    }

    public QueryRequest Build()
    {
        return new QueryRequest
        {
            PageNumber = _pageNumber,
            PageSize = _pageSize,
            SearchString = _searchString,
            FilterParams = _filterParams,
            SortingParams = _sortingParams
        };
    }
}