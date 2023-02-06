using System.Text.Json.Serialization;
using FlightManagement.Domain.Helpers;

namespace FlightManagement.Domain.Entities;

public class Airport
{
    [JsonInclude] public Guid Id { get; private set; }
    [JsonInclude] public string Name { get; private set; }
    [JsonInclude] public Address Address { get; private set; }

    public static Result<Airport> Create(string name, Address address)
    {
        return Result<Airport>.Success(
            new Airport
            {
                Id = Guid.NewGuid(),
                Name = name,
                Address = address
            }
        );
    }
}