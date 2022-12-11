using System.Text.Json.Serialization;
using FlightManagement.Domain.Helpers;

namespace FlightManagement.Domain.Entities
{
    public class City
    {
        [JsonInclude] public Guid Id { get; private set; }
        [JsonInclude] public string Name { get; private set; }
        [JsonInclude] public Country Country { get; private set; }

        public static Result<City> Create(string name, Country country)
        {
            var city = new City()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Country = country
            };

            return Result<City>.Success(city);
        }
    }
}