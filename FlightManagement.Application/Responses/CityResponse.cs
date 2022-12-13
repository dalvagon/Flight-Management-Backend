using FlightManagement.Domain.Entities;

namespace FlightManagement.Application.Responses;

public class CityResponse
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Country Country { get; private set; }
}