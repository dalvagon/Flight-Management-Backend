using System.Linq.Expressions;
using FlightManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories;

public class FlightRepository : Repository<Flight>
{
    public FlightRepository(DatabaseContext context) : base(context)
    {
    }

    public override Task<Flight?> GetAsync(Guid id)
    {
        return Context.Flights.Include(f => f.Passengers).ThenInclude(p => p.Person)
            .Include(f => f.Passengers).ThenInclude(p => p.Baggages)
            .Include(f => f.DepartureAirport).ThenInclude(a => a.Address).ThenInclude(a => a.City)
            .Include(f => f.DestinationAirport).ThenInclude(a => a.Address).ThenInclude(a => a.City)
            .Include(f => f.IntermediateStops).ThenInclude(a => a.Address)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public override async Task<IReadOnlyCollection<Flight>> FindAsync(Expression<Func<Flight, bool>> predicate)
    {
        var countries = Context.Flights.Include(f => f.Passengers).ThenInclude(p => p.Person)
            .Include(f => f.Passengers).ThenInclude(p => p.Baggages)
            .Include(f => f.DepartureAirport).ThenInclude(a => a.Address).ThenInclude(a => a.City)
            .Include(f => f.DestinationAirport).ThenInclude(a => a.Address).ThenInclude(a => a.City)
            .Include(f => f.IntermediateStops).ThenInclude(a => a.Address)
            .Where(predicate);

        return await countries.ToListAsync();
    }

    public override async Task<IReadOnlyCollection<Flight>> AllAsync()
    {
        return await Context.Flights.Include(f => f.Passengers).ThenInclude(p => p.Person)
            .Include(f => f.Passengers).ThenInclude(p => p.Baggages)
            .Include(f => f.DepartureAirport).ThenInclude(a => a.Address).ThenInclude(a => a.City)
            .Include(f => f.DestinationAirport).ThenInclude(a => a.Address).ThenInclude(a => a.City)
            .Include(f => f.IntermediateStops).ThenInclude(a => a.Address)
            .ToListAsync();
    }
}