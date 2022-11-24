using FlightManagement.Domain.Entities;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories
{
    public class BaggageRepository : Repository<Baggage>
    {
        public BaggageRepository(DatabaseContext context) : base(context)
        {
        }
    }
}