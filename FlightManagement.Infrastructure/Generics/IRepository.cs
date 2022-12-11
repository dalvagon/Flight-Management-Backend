using System.Linq.Expressions;

namespace FlightManagement.Infrastructure.Generics
{
    public interface IRepository<T>
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> GetAsync(Guid id);
        Task<IReadOnlyCollection<T>> AllAsync();
        Task<IReadOnlyCollection<T>> FindAsync(Expression<Func<T, bool>> predicate);
        void DeleteAsync(T Enity);
        void SaveChangesAsync();
    }
}