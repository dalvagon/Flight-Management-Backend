namespace FlightManagement.Application.Responses
{
    public class PassengerResponse
    {
        public Guid Id { get; private set; }
        public PersonResponse Person { get; private set; }
        public FlightResponse Flight { get; private set; }
        public double Weight { get; private set; }
        public List<AllergyResponse> Allergies { get; private set; } = new();
        public List<BaggageResponse> Baggages { get; private set; } = new();
    }
}