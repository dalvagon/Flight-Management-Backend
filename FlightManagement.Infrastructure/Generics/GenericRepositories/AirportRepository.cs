using FlightManagement.Domain.Entities;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories
{
    public class AirportRepository : Repository<Airport>
    {
        public AirportRepository(DatabaseContext context) : base(context)
        {
        }
    }
}