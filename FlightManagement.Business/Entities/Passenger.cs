using FlightManagement.Domain.Helpers;

namespace FlightManagement.Domain.Entities
{
    public class Passenger
    {
        public Guid Id { get; private set; }
        public Person Person { get; private set; }
        public Flight Flight { get; private set; }
        public double Weight { get; private set; }
        public List<Allergy> Allergies { get; private set; } = new List<Allergy>();
        public List<Baggage> Baggages { get; private set; } = new List<Baggage>();

        public static Result<Passenger> Create(Person person, Flight flight, double weight)
        {
            var passenger = new Passenger()
            {
                Id = Guid.NewGuid(),
                Person = person,
                Flight = flight,
                Weight = weight
            };

            flight.AttachPassengersToFlight(new List<Passenger> { passenger });

            return Result<Passenger>.Success(passenger);
        }

        public void AttachToFlight(Flight flight)
        {
            Flight = flight;
        }

        public void AttachBaggages(List<Baggage> baggages)
        {
            Baggages.AddRange(baggages);
        }

        public void AttachAllergy(Allergy allergy)
        {
            Allergies.Add(allergy);
        }

        public double GetBaggageWeight()
        {
            if (!Baggages.Any())
                return 0.0;

            return Baggages.Select(baggage => baggage.Weight).ToList().Sum();
        }
    }
}