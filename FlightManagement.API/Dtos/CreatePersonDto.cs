namespace FlightManagement.API.Dtos
{
    public class CreatePersonDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public CreateAddressDto AddressDto { get; set; }
    }
}