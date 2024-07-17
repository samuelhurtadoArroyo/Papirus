using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class FilterParamsBuilder
{
    private string _columnName;

    private string _filterValue;

    private FilterOptions _filterOption;

    public FilterParamsBuilder()
    {
        _columnName = string.Empty;
        _filterValue = string.Empty;
        _filterOption = FilterOptions.Contains;
    }

    public FilterParamsBuilder WithColumnName(string columnName)
    {
        _columnName = columnName;
        return this;
    }

    public FilterParamsBuilder WithFilterValue(string filterValue)
    {
        _filterValue = filterValue;
        return this;
    }

    public FilterParamsBuilder WithFilterOption(FilterOptions filterOption)
    {
        _filterOption = filterOption;
        return this;
    }

    public FilterParams Build()
    {
        return new FilterParams
        {
            ColumnName = _columnName,
            FilterValue = _filterValue,
            FilterOption = _filterOption
        };
    }
}