using FlightManagement.Business.Helpers;

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
        public Tuple<double, double, double> MaxBaggageDimensions { get; private set; }
        public List<Passenger> Passengers { get; private set; } = new List<Passenger>();
        public Airport DepartureAirport { get; private set; }
        public Airport DestinationAirport { get; private set; }
        public List<Airport> IntermediateStops { get; private set; }

        public Flight(
            DateTime departureDate,
            DateTime arrivalDate,
            int passengerCapacity,
            double baggageWeightCapacity,
            double maxWeightPerBaggage,
            Tuple<double, double, double> maxBaggageDimensions,
            List<Passenger> passengers,
            Airport departureAirport,
            Airport destinationAirport,
            List<Airport> intermediateStops
        )
        {
            Id = Guid.NewGuid();
            DepartureDate = departureDate;
            ArrivalDate = arrivalDate;
            PassengerCapacity = passengerCapacity;
            BaggageWeightCapacity = baggageWeightCapacity;
            MaxWeightPerBaggage = maxWeightPerBaggage;
            MaxBaggageDimensions = maxBaggageDimensions;
            Passengers = passengers;
            DepartureAirport = departureAirport;
            DestinationAirport = destinationAirport;
            IntermediateStops = intermediateStops;
        }

        public Result AttachPassengersToFlight(List<Passenger> passengers)
        {
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

                if (passenger.GetBaggageWeight() > MaxWeightPerBaggage)
                {
                    return Result.Failure("Person with id " + passenger.Id + " carries weight above the limit " + MaxWeightPerBaggage);
                }

                foreach (var baggage in passenger.Baggages)
                {
                    var dimensions = baggage.Dimensions;

                    if (dimensions.Item1 > MaxBaggageDimensions.Item1 || dimensions.Item2 > MaxBaggageDimensions.Item2 || dimensions.Item3 > MaxBaggageDimensions.Item3)
                    {
                        return Result.Failure("The baggage with id " + baggage.Id + " of passeger with id " + passenger.Id + " has dimensions above the limit of " + MaxBaggageDimensions.Item1 + " - " + MaxBaggageDimensions.Item2 + " - " + MaxBaggageDimensions.Item3);
                    }
                }
            }


            passengers.ForEach(passenger => passenger.AttachToFlight(this);
            Passengers.AddRange(passengers);
            return Result.Success();
        }
    }
}
