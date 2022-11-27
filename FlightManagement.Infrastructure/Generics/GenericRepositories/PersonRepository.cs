using FlightManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories
{
    public class PersonRepository : Repository<Person>
    {
        public PersonRepository(DatabaseContext context) : base(context)
        {
        }

        public override Person Get(Guid id)
        {
            return Context.People
                .Include(p => p.Address)
                .Where(p => p.Id == id)
                .FirstOrDefault();
        }

        public override IEnumerable<Person> All()
        {
            return Context.People
                .Include(p => p.Address)
                .ToList();
        }
    }
}