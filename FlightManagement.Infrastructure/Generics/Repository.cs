using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Infrastructure.Generics;

public abstract class Repository<T> : IRepository<T> where T : class
{
    protected DatabaseContext Context;

    protected Repository(DatabaseContext context)
    {
        Context = context;
    }

    public virtual async Task<T?> AddAsync(T entity)
    {
        await Context.AddAsync(entity);

        return entity;
    }

    public virtual async Task<IReadOnlyCollection<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await Context.Set<T>().AsQueryable().Where(predicate).ToListAsync();
    }

    public virtual async Task<T?> GetAsync(Guid id)
    {
        return await Context.FindAsync<T>(id);
    }

    public virtual async Task<IReadOnlyCollection<T>> AllAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public virtual T Update(T entity)
    {
        return Context.Update(entity).Entity;
    }

    public virtual void Delete(T entity)
    {
        Context.Remove(entity);
    }

    public async void SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
    }
}