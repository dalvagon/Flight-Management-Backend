namespace FlightManagement.Application.Responses
{
    public class CityResponse
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public CountryResponse Country { get; private set; }
    }
}