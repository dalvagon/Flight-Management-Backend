using FlightManagement.Application.Responses;
using MediatR;

namespace FlightManagement.Application.Commands;

public class CreateAirportCommand : IRequest<AirportResponse>
{
    public string Name { get; set; }
    public Guid AddressId { get; set; }
}