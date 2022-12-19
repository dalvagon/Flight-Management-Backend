namespace FlightManagement.API.Dtos;

public class CreateAirportDto
{
    public string Name { get; set; }
    public CreateAddressDto Address { get; set; }
}