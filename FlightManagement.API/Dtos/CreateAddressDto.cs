namespace FlightManagement.API.Dtos;

public class CreateAddressDto
{
    public string Number { get; set; }
    public string Street { get; set; }
    public Guid CityId { get; set; }
    public Guid CountryId { get; set; }
}