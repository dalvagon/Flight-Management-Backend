using System.Linq.Expressions;
using FlightManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories;

public class CityRepository : Repository<City>
{
    public CityRepository(DatabaseContext context) : base(context)
    {
    }

    public override Task<City?> GetAsync(Guid id)
    {
        return Context.Cities
            .Include(c => c.Country)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public override async Task<IReadOnlyCollection<City>> FindAsync(Expression<Func<City, bool>> predicate)
    {
        return await Context.Cities.Include(c => c.Country)
            .Where(predicate).ToListAsync();
    }
}