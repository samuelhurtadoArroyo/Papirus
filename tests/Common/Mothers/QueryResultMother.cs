using Papirus.WebApi.Domain.Define.Enums;
using System.Globalization;

namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class QueryResultMother<TModel> where TModel : EntityBase
{
    public static QueryResult<TModel> Create(List<TModel> items, PaginationData paginationData)
    {
        return new QueryResultBuilder<TModel>()
               .WithItems(items)
               .WithPaginationData(paginationData)
               .Build();
    }

    public static QueryResult<TModel> Create(IEnumerable<TModel> items, QueryRequest queryRequest)
    {
        int pageNumber = queryRequest.PageNumber ?? PaginationConst.DefaultPageNumber;
        int pageSize = queryRequest.PageSize ?? PaginationConst.DefaultPageSize;

        items = ApplySearch(items, queryRequest.SearchString);

        items = ApplyFilter(items, queryRequest.FilterParams!.ToList());

        int totalCount = items.Count();

        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        var orderedItems = ApplySortOrder(items, queryRequest.SortingParams!.ToList());

        items = orderedItems.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        PaginationData paginationData = new()
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };

        return Create(items.ToList(), paginationData);
    }

    private static IEnumerable<TModel> ApplySearch(IEnumerable<TModel> items, string? searchString)
    {
        if (!string.IsNullOrEmpty(searchString))
        {
            var properties = typeof(TModel).GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                                         .Where(p => p.PropertyType == typeof(string));

            return items.Where(item => properties.Any(prop => (prop.GetValue(item, null)!).ToString()!.ToLower().Contains(searchString.ToLower()))).ToList();
        }

        return items;
    }

    private static IEnumerable<TModel> ApplyFilter(IEnumerable<TModel> items, IEnumerable<FilterParams>? filterParams)
    {
        if (filterParams is not null)
        {
            var distinctColumns = filterParams!.Where(x => !string.IsNullOrEmpty(x.ColumnName)).Select(x => x.ColumnName).Distinct();

            foreach (string colName in distinctColumns)
            {
                var filterColumn = typeof(TModel).GetProperty(colName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);

                if (filterColumn != null)
                {
                    IEnumerable<FilterParams> filterValues = filterParams!.Where(x => x.ColumnName.Equals(colName)).Distinct();

                    if (filterValues.Count() > 1)
                    {
                        IEnumerable<TModel> sameColData = [];

                        foreach (var val in filterValues)
                        {
                            sameColData = sameColData.Concat(FilterData(items, filterColumn, val.FilterOption, val.FilterValue));
                        }
                        items = items.Intersect(sameColData);
                    }
                    else
                    {
                        items = FilterData(items, filterColumn, filterValues.FirstOrDefault()!.FilterOption, filterValues.FirstOrDefault()!.FilterValue);
                    }
                }
            }
        }

        return items;
    }

    private static IEnumerable<TModel> FilterData(IEnumerable<TModel> items, PropertyInfo filterColumn, FilterOptions filterOption, string filterValue)
    {
        int outValue;
        DateTime dateValue;

        switch (filterOption)
        {
            #region [StringDataType]

            case FilterOptions.StartsWith:
                items = items.Where(x => filterColumn.GetValue(x, null) != null && filterColumn.GetValue(x, null)!.ToString()!.ToLower().StartsWith(filterValue.ToLower()));
                break;

            case FilterOptions.EndsWith:
                items = items.Where(x => filterColumn.GetValue(x, null) != null && filterColumn.GetValue(x, null)!.ToString()!.ToLower().EndsWith(filterValue.ToLower()));
                break;

            case FilterOptions.Contains:
                items = items.Where(x => filterColumn.GetValue(x, null) != null && filterColumn.GetValue(x, null)!.ToString()!.ToLower().Contains(filterValue.ToLower()));
                break;

            case FilterOptions.DoesNotContain:
                items = items.Where(x => filterColumn.GetValue(x, null) != null && !filterColumn.GetValue(x, null)!.ToString()!.ToLower().Contains(filterValue.ToLower()));
                break;

            case FilterOptions.IsEmpty:
                items = items.Where(x => filterColumn.GetValue(x, null) != null && filterColumn.GetValue(x, null)!.ToString()!.Length == 0);
                break;

            case FilterOptions.IsNotEmpty:
                items = items.Where(x => filterColumn.GetValue(x, null) != null && filterColumn.GetValue(x, null)!.ToString()! != string.Empty);
                break;

            #endregion [StringDataType]

            #region [Custom]

            case FilterOptions.IsGreaterThan:
                if ((filterColumn.PropertyType == typeof(int) || filterColumn.PropertyType == typeof(int?)) && int.TryParse(filterValue, out outValue))
                {
                    items = items.Where(x => Convert.ToInt32(filterColumn.GetValue(x, null)) > outValue);
                }
                else if ((filterColumn.PropertyType == typeof(DateTime?)) && DateTime.TryParseExact(filterValue, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                {
                    items = items.Where(x => Convert.ToDateTime(filterColumn.GetValue(x, null)) > dateValue);
                }
                break;

            case FilterOptions.IsGreaterThanOrEqualTo:
                if ((filterColumn.PropertyType == typeof(int) || filterColumn.PropertyType == typeof(int?)) && int.TryParse(filterValue, out outValue))
                {
                    items = items.Where(x => Convert.ToInt32(filterColumn.GetValue(x, null)) >= outValue);
                }
                else if ((filterColumn.PropertyType == typeof(DateTime?)) && DateTime.TryParseExact(filterValue, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                {
                    items = items.Where(x => Convert.ToDateTime(filterColumn.GetValue(x, null)) >= dateValue);
                    break;
                }
                break;

            case FilterOptions.IsLessThan:
                if ((filterColumn.PropertyType == typeof(int) || filterColumn.PropertyType == typeof(int?)) && int.TryParse(filterValue, out outValue))
                {
                    items = items.Where(x => Convert.ToInt32(filterColumn.GetValue(x, null)) < outValue);
                }
                else if ((filterColumn.PropertyType == typeof(DateTime?)) && DateTime.TryParseExact(filterValue, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                {
                    items = items.Where(x => Convert.ToDateTime(filterColumn.GetValue(x, null)) < dateValue);
                    break;
                }
                break;

            case FilterOptions.IsLessThanOrEqualTo:
                if ((filterColumn.PropertyType == typeof(int) || filterColumn.PropertyType == typeof(int?)) && int.TryParse(filterValue, out outValue))
                {
                    items = items.Where(x => Convert.ToInt32(filterColumn.GetValue(x, null)) <= outValue);
                }
                else if ((filterColumn.PropertyType == typeof(DateTime?)) && DateTime.TryParseExact(filterValue, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                {
                    items = items.Where(x => Convert.ToDateTime(filterColumn.GetValue(x, null)) <= dateValue);
                    break;
                }
                break;

            case FilterOptions.IsEqualTo:
                if (filterValue?.Length == 0)
                {
                    items = items.Where(x => filterColumn.GetValue(x, null) != null && filterColumn.GetValue(x, null)!.ToString()?.Length == 0);
                }
                else
                {
                    if ((filterColumn.PropertyType == typeof(int) || filterColumn.PropertyType == typeof(int?)) && int.TryParse(filterValue, out outValue))
                    {
                        items = items.Where(x => Convert.ToInt32(filterColumn.GetValue(x, null)) == outValue);
                    }
                    else if ((filterColumn.PropertyType == typeof(DateTime?)) && DateTime.TryParseExact(filterValue, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                    {
                        items = items.Where(x => Convert.ToDateTime(filterColumn.GetValue(x, null)) == dateValue);
                        break;
                    }
                    else
                    {
                        items = items.Where(x => filterColumn.GetValue(x, null) != null && filterColumn.GetValue(x, null)!.ToString()!.ToLower() == filterValue?.ToLower());
                    }
                }
                break;

            case FilterOptions.IsNotEqualTo:
                if ((filterColumn.PropertyType == typeof(int) || filterColumn.PropertyType == typeof(int?)) && int.TryParse(filterValue, out outValue))
                {
                    items = items.Where(x => Convert.ToInt32(filterColumn.GetValue(x, null)) != outValue);
                }
                else if ((filterColumn.PropertyType == typeof(DateTime?)) && DateTime.TryParseExact(filterValue, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
                {
                    items = items.Where(x => Convert.ToDateTime(filterColumn.GetValue(x, null)) != dateValue);
                    break;
                }
                else
                {
                    items = items.Where(x => filterColumn.GetValue(x, null) != null && filterColumn.GetValue(x, null)!.ToString()!.ToLower() != filterValue.ToLower());
                }
                break;

                #endregion [Custom]
        }
        return items;
    }

    public static IEnumerable<TModel> ApplySortOrder(IEnumerable<TModel> items, IEnumerable<SortingParams>? sortingParams)
    {
        IOrderedEnumerable<TModel> sortedData = null!;
        foreach (var sortingParam in sortingParams!.Where(x => !String.IsNullOrEmpty(x.ColumnName)))
        {
            var col = typeof(TModel).GetProperty(sortingParam.ColumnName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
            if (col != null)
            {
                sortedData = sortedData == null ? sortingParam.SortOrder == SortOrders.Asc ? items.OrderBy(x => col.GetValue(x, null))
                                                                                           : items.OrderByDescending(x => col.GetValue(x, null))
                                                : sortingParam.SortOrder == SortOrders.Asc ? sortedData.ThenBy(x => col.GetValue(x, null))
                                                                                           : sortedData.ThenByDescending(x => col.GetValue(x, null));
            }
        }
        return sortedData ?? items.OrderBy(x => x.Id);
    }
}