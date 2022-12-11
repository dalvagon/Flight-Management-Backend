namespace FlightManagement.Application.Responses
{
    public class AirportResponse
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public AddressResponse Address { get; private set; }
    }
}