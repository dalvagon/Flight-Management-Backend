using FlightManagement.Domain.Entities;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories
{
    public class FLightRepository : Repository<Flight>
    {
        public FLightRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
