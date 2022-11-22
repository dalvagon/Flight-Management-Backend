namespace FlightManagement.Business.Entities
{
    public class Address
    {
        public Guid Id { get; private set; }
        public string Number { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }

        public Address(string number, string street, string city, string country)
        {
            Id = Guid.NewGuid();
            Number = number;
            Street = street;
            City = city;
            Country = country;
        }
    }
}
