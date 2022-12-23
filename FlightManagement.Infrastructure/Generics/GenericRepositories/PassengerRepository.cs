using System.Linq.Expressions;
using FlightManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories;

public class PassengerRepository : Repository<Passenger>
{
    public PassengerRepository(DatabaseContext context) : base(context)
    {
    }

    public override Task<Passenger?> GetAsync(Guid id)
    {
        return Context.Passengers
            .Include(p => p.Person)
            .Include(p => p.Person).ThenInclude(p => p.Address)
            .Include(p => p.Flight)
            .Include(p => p.Allergies)
            .Include(p => p.Baggages)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public override async Task<IReadOnlyCollection<Passenger>> AllAsync()
    {
        return await Context.Passengers.Include(p => p.Person)
            .Include(p => p.Flight)
            .Include(p => p.Allergies)
            .Include(p => p.Baggages)
            .ToListAsync();
    }
}