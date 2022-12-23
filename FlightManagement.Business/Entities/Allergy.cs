using System.Text.Json.Serialization;
using FlightManagement.Domain.Helpers;

namespace FlightManagement.Domain.Entities;

public class Allergy
{
    [JsonInclude] public Guid Id { get; private set; }
    [JsonInclude] public string Name { get; private set; }


    public static Result<Allergy> Create(string name)
    {
        var alergy = new Allergy
        {
            Id = Guid.NewGuid(),
            Name = name
        };

        return Result<Allergy>.Success(alergy);
    }
}