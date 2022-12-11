namespace FlightManagement.Application.Responses
{
    public class AddressResponse
    {
        public Guid Id { get; private set; }
        public string Number { get; private set; }
        public string Street { get; private set; }
        public CityResponse City { get; private set; }
        public CountryResponse Country { get; private set; }
    }
}