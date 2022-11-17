namespace FlightManagement.Business.Entities
{
    public class Address
    {
        public Guid id { get; private set; }
        public string Number { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
    }
}
