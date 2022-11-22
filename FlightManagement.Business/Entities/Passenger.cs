using FlightManagement.Business.Entities;

namespace FlightManagement.Domain.Entities
{
    public class Passenger
    {
        public Guid Id { get; private set; }
        public Person Person { get; private set; }
        public Flight Flight { get; private set; }
        public double Weight { get; private set; }
        public List<Allergy> Allergies { get; private set; }
        public List<Baggage> Baggages { get; private set; }

        public Passenger(
            Person person,
            Flight flight,
            double weight,
            List<Allergy> allergies,
            List<Baggage> baggages
        )
        {
            Id = Guid.NewGuid();
            Person = person;
            Flight = flight;
            Weight = weight;
            Allergies = allergies;
            Baggages = baggages;
        }

        public void AttachToFlight(Flight flight)
        {
            Flight = flight;
        }

        public double GetBaggageWeight()
        {
            return Baggages.Select(baggage => baggage.Weight).ToList().Sum();
        }
    }
}
