using Papirus.WebApi.Domain.Define.Enums;
using System.Linq.Dynamic.Core;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

[assembly: InternalsVisibleTo("Papirus.WebApi.Infrastructure.UnitTests")]

namespace Papirus.WebApi.Infrastructure.Common;

[ExcludeFromCodeCoverage]
public class Repository<T> : IRepository<T> where T : EntityBase
{
    private readonly AppDbContext _appDbContext;

    public Repository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _appDbContext.Set<T>().AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _appDbContext.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _appDbContext.Set<T>().ToListAsync();
    }

    public virtual async Task<ICollection<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] propertySelectors)
    {
        var query = _appDbContext.Set<T>().AsQueryable();

        foreach (var selector in propertySelectors)
        {
            query = query.Include(selector);
        }

        return await query.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return (await _appDbContext.Set<T>()
                                   .AsNoTracking()
                                   .AsQueryable()
                                   .FirstOrDefaultAsync(x => x.Id == id))!;
    }

    public virtual async Task<T?> GetByIdIncludingAsync(int id, params Expression<Func<T, object>>[] includeProperties)
    {
        var query = _appDbContext.Set<T>().AsQueryable();

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return await query.FirstOrDefaultAsync(entity => entity.Id.Equals(id));
    }

    public async Task RemoveAsync(T entity)
    {
        _appDbContext.Set<T>().Remove(entity!);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _appDbContext.Set<T>().Update(entity!);
        await _appDbContext.SaveChangesAsync();
        return entity!;
    }

    public async Task<QueryResult<T>> GetByQueryRequestAsync(QueryRequest queryRequest)
    {
        var queryItems = _appDbContext.Set<T>().AsQueryable();

        // Include nested properties for filtering, searching, and sorting
        queryItems = IncludeNestedProperties(queryItems);

        // Apply search
        if (!string.IsNullOrEmpty(queryRequest.SearchString))
        {
            queryItems = ApplySearch(queryItems, queryRequest.SearchString);
        }

        // Apply filters
        if (queryRequest.FilterParams != null)
        {
            queryItems = ApplyFilter(queryItems, queryRequest.FilterParams);
        }

        // Apply sorting
        if (queryRequest.SortingParams != null)
        {
            queryItems = ApplySortOrder(queryItems, queryRequest.SortingParams);
        }

        // Apply pagination
        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;
        int totalCount = await queryItems.CountAsync();

        var items = await queryItems.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new QueryResult<T>
        {
            Items = items,
            PaginationData = new PaginationData
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            }
        };
    }

    public async Task<QueryResult<T>> GetByQueryRequestIncludingAsync(IQueryable<T> query, QueryRequest queryRequest)
    {
        int pageNumber = queryRequest.PageNumber ?? PaginationConst.DefaultPageNumber;
        int pageSize = queryRequest.PageSize ?? PaginationConst.DefaultPageSize;
        var itemsResult = ApplySearch(query, queryRequest.SearchString);

        itemsResult = ApplyFilter(itemsResult, queryRequest!.FilterParams);

        int totalCount = await itemsResult.CountAsync();
        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        if (queryRequest.SortingParams != null)
            itemsResult = ApplySortOrder(itemsResult, queryRequest.SortingParams);

        var items = await itemsResult.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new()
        {
            Items = items,
            PaginationData = new()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages
            }
        };
    }

    private static IQueryable<T> ApplySearch(IQueryable<T> items, string? searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString)) return items;
        var searchQueries = GetSearchQueries(typeof(T), null);
        var combinedQuery = string.Join(" OR ", searchQueries);
        return items.Where(combinedQuery, searchString);
    }

    private static IQueryable<T> ApplyFilter(IQueryable<T> items, IEnumerable<FilterParams>? filterParams)
    {
        if (filterParams == null) return items;
        foreach (var filter in filterParams!)
        {
            // Check for null filters
            if (filter.FilterOption == FilterOptions.IsNull)
            {
                items = items.Where($"{filter.ColumnName} == null");
            }
            else if (filter.FilterOption == FilterOptions.IsNotNull)
            {
                items = items.Where($"{filter.ColumnName} != null");
            }
            else
            {
                items = filter.FilterOption switch
                {
                    FilterOptions.StartsWith => items.Where($"{filter.ColumnName}.StartsWith(@0)", filter.FilterValue),
                    FilterOptions.EndsWith => items.Where($"{filter.ColumnName}.EndsWith(@0)", filter.FilterValue),
                    FilterOptions.Contains => items.Where($"{filter.ColumnName}.Contains(@0)", filter.FilterValue),
                    FilterOptions.DoesNotContain => items.Where($"!{filter.ColumnName}.Contains(@0)", filter.FilterValue),
                    FilterOptions.IsEmpty => items.Where($"{filter.ColumnName} == \"\""),
                    FilterOptions.IsNotEmpty => items.Where($"{filter.ColumnName} != \"\""),
                    FilterOptions.IsEqualTo => items.Where($"{filter.ColumnName} == @0", filter.FilterValue),
                    FilterOptions.IsNotEqualTo => items.Where($"{filter.ColumnName} != @0", filter.FilterValue),
                    FilterOptions.IsGreaterThan => items.Where($"{filter.ColumnName} > @0", filter.FilterValue),
                    FilterOptions.IsGreaterThanOrEqualTo => items.Where($"{filter.ColumnName} >= @0", filter.FilterValue),
                    FilterOptions.IsLessThan => items.Where($"{filter.ColumnName} < @0", filter.FilterValue),
                    FilterOptions.IsLessThanOrEqualTo => items.Where($"{filter.ColumnName} <= @0", filter.FilterValue),
                    _ => items
                };
            }
        }
        return items;
    }

    private static IQueryable<T> ApplySortOrder(IQueryable<T> items, IEnumerable<SortingParams> sortingParams)
    {
        if (sortingParams?.Any() == true)
        {
            var sortingQuery = string.Join(", ", sortingParams.Select(s => $"{s.ColumnName} {(s.SortOrder == SortOrders.Asc ? "ascending" : "descending")}"));
            items = items.OrderBy(sortingQuery);
        }
        return items;
    }

    private static List<string> GetIncludePaths(Type entityType)
    {
        var includePaths = new List<string>();
        foreach (var property in entityType.GetProperties())
        {
            if (property.GetCustomAttribute<IncludeAttribute>() != null)
            {
                includePaths.Add(property.Name);
                if (property.PropertyType.IsClass && property.PropertyType != typeof(string) && property.PropertyType != typeof(T))
                {
                    foreach (var nestedPath in GetIncludePaths(property.PropertyType))
                    {
                        includePaths.Add($"{property.Name}.{nestedPath}");
                    }
                }
            }
        }
        return includePaths;
    }

    private static List<string> GetSearchQueries(Type type, string? parentProperty)
    {
        var searchQueries = new List<string>();
        var properties = type.GetProperties();

        foreach (var property in properties)
        {
            if (property.PropertyType == typeof(string))
            {
                var propertyName = parentProperty == null ? property.Name : $"{parentProperty}.{property.Name}";
                searchQueries.Add($"{propertyName}.Contains(@0)");
            }
            else if (property.PropertyType.IsClass && property.PropertyType != typeof(string) && property.PropertyType != typeof(T))
            {
                var nestedParent = parentProperty == null ? property.Name : $"{parentProperty}.{property.Name}";
                searchQueries.AddRange(GetSearchQueries(property.PropertyType, nestedParent));
            }
        }

        return searchQueries;
    }

    private static IQueryable<T> IncludeNestedProperties(IQueryable<T> query)
    {
        var includePaths = GetIncludePaths(typeof(T));
        foreach (var path in includePaths)
        {
            query = query.Include(path);
        }
        return query;
    }
}