using System.Linq.Expressions;

namespace FlightManagement.Infrastructure.Generics;

public interface IRepository<T>
{
    Task<T?> AddAsync(T entity);
    T Update(T entity);
    Task<T?> GetAsync(Guid id);
    Task<IReadOnlyCollection<T>> AllAsync();
    Task<IReadOnlyCollection<T>> FindAsync(Expression<Func<T, bool>> predicate);
    void Delete(T entity);
    void SaveChangesAsync();
}