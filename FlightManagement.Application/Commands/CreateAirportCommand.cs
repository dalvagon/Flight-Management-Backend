using FlightManagement.Application.Responses;
using FlightManagement.Domain.Helpers;
using MediatR;

namespace FlightManagement.Application.Commands
{
    public class CreateAirportCommand : IRequest<Result<AirportResponse>>
    {
        public string Name { get; set; }
        public CreateAddressCommand Address { get; set; }
    }
}