using FlightManagement.Application.Responses;
using FlightManagement.Domain.Helpers;
using MediatR;

namespace FlightManagement.Application.Queries
{
    public class GetPassengerQuery : IRequest<Result<PassengerResponse>>
    {
        public Guid PassengerId { get; set; }
    }
}