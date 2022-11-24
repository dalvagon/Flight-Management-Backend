using FlightManagement.Domain.Helpers;
using FlightManagement.DOmain.Helpers;

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
        public List<Passenger> Passengers { get; private set; } = new List<Passenger>();
        public Airport DepartureAirport { get; private set; }
        public Airport DestinationAirport { get; private set; }
        public List<Airport> IntermediateStops { get; private set; } = new List<Airport>();

        public static Result<Flight> Create(
            DateTime departureDate,
            DateTime arrivalDate,
            int passengerCapacity,
            double baggageWeightCapacity,
            double maxWeightPerBaggage,
            double maxBaggageWeightPerPassenger,
            double maxBaggageWidth,
            double maxBaggageHeight,
            double maxBaggageLenght,
            Airport departureAirport,
            Airport destinationAirport
        )
        {
            return Result<Flight>.Success(
                new Flight()
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
                    MaxBaggageLength = maxBaggageLenght,
                    DepartureAirport = departureAirport,
                    DestinationAirport = destinationAirport
                }
            );
        }

        public Result AttachPassengersToFlight(List<Passenger> passengers)
        {
            double totalWeigth = 0;

            foreach (var passenger in passengers)
            {
                if (Passengers.Contains(passenger))
                {
                    return Result.Failure(
                        "Person with id "
                            + passenger.Id
                            + " is already a passenger in flight with id "
                            + Id
                    );
                }

                if (passenger.GetBaggageWeight() > MaxBaggageWeightPerPassenger)
                {
                    return Result.Failure(
                        "Person with id "
                            + passenger.Id
                            + " carries weight above the limit "
                            + MaxBaggageWeightPerPassenger
                    );
                }

                foreach (var baggage in passenger.Baggages)
                {
                    totalWeigth += baggage.Weight;

                    if (
                        baggage.Width > MaxBaggageWidth
                        || baggage.Height > MaxBaggageHeight
                        || baggage.Length > MaxBaggageLength
                    )
                    {
                        return Result.Failure(
                            "The baggage with id "
                                + baggage.Id
                                + " of passeger with id "
                                + passenger.Id
                                + " has dimensions above the limit of "
                                + MaxBaggageWidth
                                + " - "
                                + MaxBaggageHeight
                                + " - "
                                + MaxBaggageLength
                        );
                    }

                    if (baggage.Weight > MaxWeightPerBaggage)
                    {
                        return Result.Failure(
                            "The baggage with id "
                                + baggage.Id
                                + " of passeger with id "
                                + passenger.Id
                                + " has weight above the limit of "
                                + MaxWeightPerBaggage
                        );
                    }
                }
            }

            if (totalWeigth > BaggageWeightCapacity)
            {
                return Result.Failure(
                    "The baggage weight "
                        + totalWeigth
                        + " exceeds the limit "
                        + BaggageWeightCapacity
                );
            }

            passengers.ForEach(passenger => passenger.AttachToFlight(this));
            Passengers.AddRange(passengers);
            return Result.Success();
        }
    }
}
