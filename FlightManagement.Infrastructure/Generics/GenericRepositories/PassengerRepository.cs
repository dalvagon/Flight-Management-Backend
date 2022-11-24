using FlightManagement.Domain.Entities;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories
{
    public class PassengerRepository : Repository<Passenger>
    {
        public PassengerRepository(DatabaseContext context) : base(context)
        {
        }
    }
}