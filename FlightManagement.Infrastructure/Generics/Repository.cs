using System.Linq.Expressions;

namespace FlightManagement.Infrastructure.Generics
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected DatabaseContext Context;

        public Repository(DatabaseContext context)
        {
            Context = context;
        }

        public virtual T Add(T entity)
        {
            return Context.Add(entity).Entity;
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().AsQueryable().Where(predicate).ToList();
        }

        public virtual T Get(Guid id)
        {
            return Context.Find<T>(id);
        }

        public virtual IEnumerable<T> All()
        {
            return Context.Set<T>().ToList();
        }

        public virtual T Update(T entity)
        {
            return Context.Update(entity).Entity;
        }

        public virtual void Delete(T entity)
        {
            Context.Remove(entity);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}