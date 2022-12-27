using FlightManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories;

public class PersonRepository : Repository<Person>
{
    public PersonRepository(DatabaseContext context) : base(context)
    {
    }

    public override Task<Person?> GetAsync(Guid id)
    {
        return Context.People
            .Include(p => p.Address).ThenInclude(a => a.City)
            .Include(p => p.Address).ThenInclude(a => a.Country)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IReadOnlyCollection<Person>> AllAsync()
    {
        return await Context.People
            .Include(p => p.Address).ThenInclude(a => a.City)
            .Include(p => p.Address).ThenInclude(a => a.Country)
            .ToListAsync();
    }
}