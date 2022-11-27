using FlightManagement.Domain.Helpers;

namespace FlightManagement.Domain.Entities
{
    public class Flight
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
        public List<Passenger> Passengers { get; private set; } = new();
        public Airport DepartureAirport { get; private set; }
        public Airport DestinationAirport { get; private set; }
        public List<Airport> IntermediateStops { get; private set; } = new();

        public static Result<Flight> Create(
            DateTime departureDate,
            DateTime arrivalDate,
            int passengerCapacity,
            double baggageWeightCapacity,
            double maxWeightPerBaggage,
            double maxBaggageWeightPerPassenger,
            double maxBaggageWidth,
            double maxBaggageHeight,
            double maxBaggageLength,
            Airport departureAirport,
            Airport destinationAirport
        )
        {
            if (arrivalDate.CompareTo(departureDate) < 0)
            {
                return Result<Flight>.Failure(
                    $"The arrival date {arrivalDate} for the flight is past the departure date {departureDate}");
            }

            return Result<Flight>.Success(
                new Flight
                {
                    Id = Guid.NewGuid(),
                    DepartureDate = departureDate,
                    ArrivalDate = arrivalDate,
                    PassengerCapacity = passengerCapacity,
                    BaggageWeightCapacity = baggageWeightCapacity,
                    MaxWeightPerBaggage = maxWeightPerBaggage,
                    MaxBaggageWeightPerPassenger = maxBaggageWeightPerPassenger,
                    MaxBaggageWidth = maxBaggageWidth,
                    MaxBaggageHeight = maxBaggageHeight,
                    MaxBaggageLength = maxBaggageLength,
                    DepartureAirport = departureAirport,
                    DestinationAirport = destinationAirport
                }
            );
        }

        public Result<Passenger> AttachPassengerToFlight(Passenger passenger)
        {
            var people = Passengers.Select(p => p.Person);

            if (people.Contains(passenger.Person))
            {
                return Result<Passenger>.Failure(
                    $"Person with id {passenger.Person.Id} is already a passenger in flight with id {Id}"
                );
            }

            if (passenger.GetBaggageWeight() > MaxBaggageWeightPerPassenger)
            {
                return Result<Passenger>.Failure(
                    $"Person with id {passenger.Person.Id} carries weight above the limit {MaxBaggageWeightPerPassenger}"
                );
            }

            foreach (var baggage in passenger.Baggages)
            {
                if (baggage.Weight > MaxWeightPerBaggage)
                {
                    return Result<Passenger>.Failure(
                        $"Person with id {passenger.Person.Id} carries a baggage with the weight above the limit of {MaxWeightPerBaggage}");
                }

                if (
                    baggage.Width > MaxBaggageWidth
                    || baggage.Height > MaxBaggageHeight
                    || baggage.Length > MaxBaggageLength
                )
                {
                    return Result<Passenger>.Failure(
                        $"Person with id {passenger.Person.Id} carries a baggage that has dimensions above the limit of {MaxBaggageWidth} - {MaxBaggageHeight} - {MaxBaggageLength}"
                    );
                }
            }

            passenger.Baggages.ForEach(baggage => baggage.AttachBaggageToPassenger(passenger));
            Passengers.Add(passenger);
            return Result<Passenger>.Success(passenger);
        }
    }
}