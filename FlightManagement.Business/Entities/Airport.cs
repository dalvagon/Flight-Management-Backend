using FlightManagement.Domain.Helpers;

namespace FlightManagement.Domain.Entities
{
    public class Airport
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Address Address { get; private set; }
        public string City { get; private set; }

        public static Result<Airport> Create(string name, Address address, string city)
        {
            return Result<Airport>.Success(
                new Airport
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Address = address,
                    City = city
                }
            );
        }
    }
}