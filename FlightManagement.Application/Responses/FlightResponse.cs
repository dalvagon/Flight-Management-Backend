namespace FlightManagement.Application.Responses
{
    public class FlightResponse
    {
        public Guid Id { get; private set; }
        public DateTime DepartureDate { get; private set; }
        public DateTime ArrivalDate { get; private set; }
        public int PassengerCapacity { get; private set; }
        public double BaggageWeightCapacity { get; private set; }
        public double MaxWeightPerBaggage { get; private set; }
        public double MaxBaggageWeightPerPassenger { get; private set; }
        public double MaxBaggageWidth { get; private set; }
        public double MaxBaggageHeight { get; private set; }
        public double MaxBaggageLength { get; private set; }
        public List<PassengerResponse> PassengersResponse { get; private set; } = new();
        public AirportResponse DepartureAirport { get; private set; }
        public AirportResponse DestinationAirport { get; private set; }
        public List<AirportResponse> IntermediateStops { get; private set; } = new();
    }
}