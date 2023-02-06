using FlightManagement.Application.Responses;
using FlightManagement.Domain.Helpers;
using MediatR;

namespace FlightManagement.Application.Queries
{
    public class GetAirportQuery : IRequest<Result<AirportResponse>>
    {
        public Guid AirportId { get; set; }
    }
}