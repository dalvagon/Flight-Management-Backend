namespace FlightManagement.API.Dtos
{
    public class CreateFlightDto
    {
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public int PassengerCapacity { get; set; }
        public double BaggageWeightCapacity { get; set; }
        public double MaxWeightPerBaggage { get; set; }
        public double MaxBaggageWeightPerPassenger { get; set; }
        public double MaxBaggageWidth { get; set; }
        public double MaxBaggageHeight { get; set; }
        public double MaxBaggageLength { get; set; }
        public Guid DepartureAirportId { get; set; }
        public Guid DestinationAirportId { get; set; }
    }
}
