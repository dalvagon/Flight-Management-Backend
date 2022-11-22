namespace FlightManagement.Domain.Entities
{
    public class Baggage
    {
        public Guid Id { get; private set; }
        public Passenger Passenger { get; private set; }
        public double Weight { get; private set; }
        public Tuple<double, double, double> Dimensions { get; private set; }

        public Baggage(Passenger passenger, double weight, Tuple<double, double, double> dimensions)
        {
            Id = Guid.NewGuid();
            Passenger = passenger;
            Weight = weight;
            Dimensions = dimensions;
        }
    }
}
