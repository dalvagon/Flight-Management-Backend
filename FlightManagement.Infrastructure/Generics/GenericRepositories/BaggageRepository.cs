using FlightManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories
{
    public class BaggageRepository : Repository<Baggage>
    {
        public BaggageRepository(DatabaseContext context) : base(context)
        {
        }

        public override Baggage Get(Guid id)
        {
            return Context.Baggages.Include(b => b.Passenger).FirstOrDefault();
        }

        public override IEnumerable<Baggage> All()
        {
            return Context.Baggages.Include(b => b.Passenger).ToList();
        }
    }
}