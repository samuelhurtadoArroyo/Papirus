using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.WebApi.Domain.Dtos;

[ExcludeFromCodeCoverage]
public class FilterParams
{
    public string ColumnName { get; set; } = string.Empty;

    public string FilterValue { get; set; } = string.Empty;

    public FilterOptions FilterOption { get; set; } = FilterOptions.Contains;
}