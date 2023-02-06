using FlightManagement.Domain.Helpers;
using MediatR;

namespace FlightManagement.Application.Commands
{
    public class DeleteAirportCommand : IRequest<Result>
    {
        public Guid AirportId { get; set; }
    }
}