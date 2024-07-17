namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class QueryRequestMother
{
    public static QueryRequest Create(int? pageNumber, int? pageSize, string? searchString, IEnumerable<FilterParams>? filterParams, IEnumerable<SortingParams>? sortingParams)
    {
        return new QueryRequestBuilder()
               .WithPageNumber(pageNumber)
               .WithPageSize(pageSize)
               .WithSearchString(searchString)
               .WithFilterParams(filterParams)
               .WithSortingParams(sortingParams)
               .Build();
    }

    public static QueryRequest DefaultQueryRequest()
    {
        return new QueryRequestBuilder()
               .WithPageNumber(PaginationConst.DefaultPageNumber)
               .WithPageSize(PaginationConst.DefaultPageSize)
               .WithSearchString(CommonConst.SearchAdmin)
               .Build();
    }

    public static QueryRequest GetEmptyQueryRequest()
    {
        return new QueryRequestBuilder()
               .WithPageNumber(null)
               .WithPageSize(null)
               .WithSearchString(null)
               .Build();
    }
}