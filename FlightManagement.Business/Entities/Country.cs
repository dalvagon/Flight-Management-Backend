using System.Text.Json.Serialization;
using FlightManagement.Domain.Helpers;

namespace FlightManagement.Domain.Entities
{
    public class Country
    {
        [JsonInclude] public Guid Id { get; private set; }
        [JsonInclude] public string Name { get; private set; }
        [JsonInclude] public string Code { get; private set; }

        public static Result<Country> Create(string name, string code)
        {
            var country = new Country()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Code = code
            };

            return Result<Country>.Success(country);
        }
    }
}