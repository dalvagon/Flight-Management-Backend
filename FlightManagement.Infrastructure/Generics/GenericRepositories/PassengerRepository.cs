using FlightManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories
{
    public class PassengerRepository : Repository<Passenger>
    {
        public PassengerRepository(DatabaseContext context) : base(context)
        {
        }

        public override Passenger Get(Guid id)
        {
            return Context.Passengers.Include(p => p.Person)
                .Include(p => p.Flight)
                .Include(p => p.Allergies)
                .Include(p => p.Baggages)
                .FirstOrDefault();
        }

        public override IEnumerable<Passenger> All()
        {
            return Context.Passengers.Include(p => p.Person)
                .Include(p => p.Flight)
                .Include(p => p.Allergies)
                .Include(p => p.Baggages)
                .ToList();
        }
    }
}