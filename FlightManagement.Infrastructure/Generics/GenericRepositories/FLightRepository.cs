using FlightManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories
{
    public class FLightRepository : Repository<Flight>
    {
        public FLightRepository(DatabaseContext context) : base(context)
        {
        }

        public override Flight Get(Guid id)
        {
            return Context.Flights
                .Include(f => f.Passengers)
                .Include(f => f.DepartureAirport)
                .Include(f => f.DestinationAirport)
                .Include(f => f.IntermediateStops)
                .FirstOrDefault();
        }

        public override IEnumerable<Flight> All()
        {
            return Context.Flights
                .Include(f => f.Passengers)
                .Include(f => f.DepartureAirport)
                .Include(f => f.DestinationAirport)
                .Include(f => f.IntermediateStops)
                .ToList();
        }
    }
}