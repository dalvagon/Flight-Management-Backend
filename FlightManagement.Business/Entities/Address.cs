using System.Text.Json.Serialization;
using FlightManagement.Domain.Helpers;

namespace FlightManagement.Domain.Entities;

public class Address
{
    [JsonInclude] public Guid Id { get; private set; }
    [JsonInclude] public string Number { get; private set; }
    [JsonInclude] public string Street { get; private set; }
    [JsonInclude] public City City { get; private set; }
    [JsonInclude] public Country Country { get; private set; }

    public static Result<Address> Create(string number, string street, City city, Country country)
    {
        var address = new Address
        {
            Id = Guid.NewGuid(),
            Number = number,
            Street = street,
            City = city,
            Country = country
        };

        return Result<Address>.Success(address);
    }
}