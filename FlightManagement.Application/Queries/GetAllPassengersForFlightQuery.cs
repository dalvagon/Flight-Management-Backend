using FlightManagement.Application.Responses;
using FlightManagement.Domain.Helpers;
using MediatR;

namespace FlightManagement.Application.Queries
{
    public class GetAllPassengersForFlightQuery : IRequest<Result<IReadOnlyCollection<PassengerResponse>>>
    {
        public Guid FlightId { get; set; }
    }
}