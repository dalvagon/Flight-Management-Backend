using System.Text.Json.Serialization;
using FlightManagement.Domain.Helpers;

namespace FlightManagement.Domain.Entities;

public class Baggage
{
    [JsonInclude] public Guid Id { get; private set; }
    [JsonInclude] public Passenger Passenger { get; private set; }
    [JsonInclude] public double Weight { get; private set; }
    [JsonInclude] public double Width { get; private set; }
    [JsonInclude] public double Height { get; private set; }
    [JsonInclude] public double Length { get; private set; }


    public static Result<Baggage> Create(double weight, double width, double height, double length)
    {
        var baggage = new Baggage
        {
            Id = Guid.NewGuid(),
            Weight = weight,
            Width = width,
            Height = height,
            Length = length
        };

        return Result<Baggage>.Success(baggage);
    }

    public void AttachBaggageToPassenger(Passenger passenger)
    {
        Passenger = passenger;
    }
}