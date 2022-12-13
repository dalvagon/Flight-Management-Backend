namespace FlightManagement.Application.Commands;

public class CreatePersonDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public CreateAddressCommand AddressCommand { get; set; }
}