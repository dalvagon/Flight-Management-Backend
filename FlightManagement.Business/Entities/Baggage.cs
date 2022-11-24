namespace FlightManagement.Domain.Entities
{
    public class Baggage
    {
        public Guid Id { get; private set; }
        public Passenger Passenger { get; private set; }
        public double Weight { get; private set; }
        public double Width { get; private set; }
        public double Height { get; private set; }
        public double Length { get; private set; }

        public Baggage(double weight, double width, double height, double length)
        {
            Id = Guid.NewGuid();
            Weight = weight;
            Width = width;
            Height = height;
            Length = length;
        }

        public void AttachBaggageToPassenger(Passenger passenger)
        {
            Passenger = passenger;
        }
    }
}
