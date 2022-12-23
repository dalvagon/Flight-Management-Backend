using FlightManagement.Domain.Entities;

namespace FlightManagement.Application.Responses;

public class FlightResponse
{
    public Guid Id { get; private set; }
    public DateTime DepartureDate { get; private set; }
    public DateTime ArrivalDate { get; private set; }
    public int PassengerCapacity { get; private set; }
    public double BaggageWeightCapacity { get; private set; }
    public double MaxWeightPerBaggage { get; private set; }
    public double MaxBaggageWeightPerPassenger { get; private set; }
    public double MaxBaggageWidth { get; private set; }
    public double MaxBaggageHeight { get; private set; }
    public double MaxBaggageLength { get; private set; }
    public List<Passenger> Passengers { get; } = new();
    public Airport DepartureAirport { get; private set; }
    public Airport DestinationAirport { get; private set; }
    public List<Airport> IntermediateStops { get; } = new();
}