namespace FlightManagement.API.Dtos
{
    public class CreateAirportDto
    {
        public string Name { get; set; }
        public Guid AddressId { get; set; }
    }
}