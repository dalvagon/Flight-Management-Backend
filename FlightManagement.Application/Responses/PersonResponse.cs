using FlightManagement.Domain.Entities;

namespace FlightManagement.Application.Responses
{
    public class PersonResponse
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Gender Gender { get; private set; }
        public AddressResponse Address { get; private set; }
    }
}