namespace FlightManagement.API.Dtos
{
    public class CreatePassengerDto
    {
        public Guid PersonId { get; set; }
        public Guid FlightId { get; set; }
        public double Weight { get; set; }
        public List<CreateBaggageDto> BaggageDtos { get; set; }
        public List<Guid> AllergyIds { get; set; }
    }
}