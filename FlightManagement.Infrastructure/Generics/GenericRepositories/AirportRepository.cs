using FlightManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories
{
    public class AirportRepository : Repository<Airport>
    {
        public AirportRepository(DatabaseContext context) : base(context)
        {
        }

        public override Airport Get(Guid id)
        {
            return Context.Airports.Include(airport => airport.Address)
                .Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public override IEnumerable<Airport> All()
        {
            return Context.Airports.Include(airport => airport.Address).ToList();
        }
    }
}