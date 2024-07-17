namespace Papirus.WebApi.Domain.Define;

[ExcludeFromCodeCoverage]
public static class PaginationConst
{
    public static readonly int DefaultPageNumber = 1;

    public static readonly int DefaultPageSize = 10;

    public static readonly string DefaultSortOrder = "Id";

    public static readonly string DefaultPaginationHeader = "PaginationData";
}