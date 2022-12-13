using FlightManagement.Domain.Entities;

namespace FlightManagement.Application.Responses;

public class AirportResponse
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Address Address { get; private set; }
}