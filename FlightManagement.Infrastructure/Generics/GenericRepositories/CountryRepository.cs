using FlightManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories;

public class CountryRepository : Repository<Country>
{
    public CountryRepository(DatabaseContext context) : base(context)
    {
    }

    public override Task<Country?> GetAsync(Guid id)
    {
        return Context.Countries.FirstOrDefaultAsync(c => c.Id == id);
    }
}