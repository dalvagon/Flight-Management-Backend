using FlightManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories
{
    public class FlightRepository : Repository<Flight>
    {
        public FlightRepository(DatabaseContext context) : base(context)
        {
        }

        public override Flight Get(Guid id)
        {
            return Context.Flights
                .Include(f => f.Passengers).ThenInclude(p => p.Person)
                .Include(f => f.Passengers).ThenInclude(p => p.Baggages)
                .Include(f => f.DepartureAirport).ThenInclude(a => a.Address)
                .Include(f => f.DestinationAirport).ThenInclude(a => a.Address)
                .Include(f => f.IntermediateStops).ThenInclude(a => a.Address)
                .Where(f => f.Id == id)
                .FirstOrDefault();
        }

        public override IEnumerable<Flight> All()
        {
            return Context.Flights
                .Include(f => f.Passengers).ThenInclude(p => p.Person)
                .Include(f => f.DepartureAirport)
                .Include(f => f.DestinationAirport).ThenInclude(a => a.Address)
                .Include(f => f.IntermediateStops).ThenInclude(a => a.Address)
                .ToList();
        }
    }
}