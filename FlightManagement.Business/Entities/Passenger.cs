using System.Text.Json.Serialization;
using FlightManagement.Domain.Helpers;

namespace FlightManagement.Domain.Entities
{
    public class Passenger
    {
        [JsonInclude] public Guid Id { get; private set; }
        [JsonInclude] public Person Person { get; private set; }
        [JsonInclude] public Flight Flight { get; private set; }
        [JsonInclude] public double Weight { get; private set; }
        [JsonInclude] public List<Allergy> Allergies { get; private set; } = new();
        [JsonInclude] public List<Baggage> Baggages { get; private set; } = new();

        public static Result<Passenger> Create(Person person, Flight flight, double weight, List<Baggage> baggages,
            List<Allergy> allergies)
        {
            var passenger = new Passenger
            {
                Id = Guid.NewGuid(),
                Person = person,
                Flight = flight,
                Weight = weight,
                Baggages = baggages,
                Allergies = allergies,
            };

            return flight.AttachPassengerToFlight(passenger);
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