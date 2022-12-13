using FlightManagement.Domain.Entities;

namespace FlightManagement.Application.Responses;

public class AddressResponse
{
    public Guid Id { get; private set; }
    public string Number { get; private set; }
    public string Street { get; private set; }
    public City City { get; private set; }
    public Country Country { get; private set; }
}