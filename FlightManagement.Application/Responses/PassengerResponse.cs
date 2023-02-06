using FlightManagement.Domain.Entities;

namespace FlightManagement.Application.Responses;

public class PassengerResponse
{
    public Guid Id { get; private set; }
    public Person Person { get; private set; }
    public Flight Flight { get; private set; }
    public double Weight { get; private set; }
    public List<Allergy> Allergies { get; } = new();
    public List<Baggage> Baggages { get; } = new();
}