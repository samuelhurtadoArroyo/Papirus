namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class SortingParamsMother
{
    public static SortingParams Create(string columnName, SortOrders sortOrders)
    {
        return new SortingParamsBuilder()
            .WithColumnName(columnName)
            .WithSortOrder(sortOrders)
            .Build();
    }

    public static IEnumerable<SortingParams> GetSortingParams(string columnName, SortOrders sortOrders = SortOrders.Asc)
    {
        return [Create(columnName, sortOrders)];
    }
}