using FlightManagement.Business.Entities;

namespace FlightManagement.Domain.Entities
{
    public class Airport
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Address Address { get; private set; }
        public String City { get; private set; }

        public Airport(string name, Address address, string city)
        {
            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            City = city;
        }
    }
}
