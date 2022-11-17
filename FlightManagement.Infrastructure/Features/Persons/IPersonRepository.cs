using FlightManagement.Business.Entities;

namespace FlightManagement.API.Features.Persons
{
    public interface IPersonRepository
    {
        void Add(Person person);
        IEnumerable<Person> GetAll();
    }
}