namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class SortingParamsBuilder
{
    private SortOrders _sortOrder;

    private string _columnName;

    public SortingParamsBuilder()
    {
        _columnName = string.Empty;
        _sortOrder = SortOrders.Asc;
    }

    public SortingParamsBuilder WithColumnName(string columnName)
    {
        _columnName = columnName;
        return this;
    }

    public SortingParamsBuilder WithSortOrder(SortOrders sortOrder)
    {
        _sortOrder = sortOrder;
        return this;
    }

    public SortingParams Build()
    {
        return new SortingParams
        {
            SortOrder = _sortOrder,
            ColumnName = _columnName
        };
    }
}