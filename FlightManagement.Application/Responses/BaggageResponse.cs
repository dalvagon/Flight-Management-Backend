namespace FlightManagement.Application.Responses
{
    public class BaggageResponse
    {
        public Guid Id { get; private set; }
        public PassengerResponse Passenger { get; private set; }
        public double Weight { get; private set; }
        public double Width { get; private set; }
        public double Height { get; private set; }
        public double Length { get; private set; }
    }
}