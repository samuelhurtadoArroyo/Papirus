namespace Papirus.WebApi.Domain.Common;

public interface IRepository<T> where T : EntityBase
{
    public Task<T> AddAsync(T entity);

    public Task<IEnumerable<T>> GetAllAsync();

    Task<ICollection<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] propertySelectors);

    public Task<T> GetByIdAsync(int id);

    Task<T?> GetByIdIncludingAsync(int id, params Expression<Func<T, object>>[] includeProperties);

    public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    public Task<T> UpdateAsync(T entity);

    public Task RemoveAsync(T entity);

    public Task<QueryResult<T>> GetByQueryRequestAsync(QueryRequest queryRequest);
}