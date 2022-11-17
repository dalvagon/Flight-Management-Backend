using FlightManagement.API.Data;
using FlightManagement.Business.Entities;

namespace FlightManagement.API.Features.Persons
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DatabaseContext context = new DatabaseContext();

        public void Add(Person person)
        {
            context.Set<Person>().Add(person);
        }
        public IEnumerable<Person> GetAll()
        {
            return context.Set<Person>().ToList();
        }

    }
}
