using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class FilterParamsMother
{
    public static FilterParams Create(string columnName, FilterOptions filterOption, string filerValue)
    {
        return new FilterParamsBuilder()
            .WithColumnName(columnName)
            .WithFilterOption(filterOption)
            .WithFilterValue(filerValue)
            .Build();
    }

    public static IEnumerable<FilterParams> GetFilterParams(string columnName, FilterOptions filterOption, string filerValue)
    {
        return [Create(columnName, filterOption, filerValue)];
    }
}