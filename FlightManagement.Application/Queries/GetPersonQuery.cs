using FlightManagement.Application.Responses;
using FlightManagement.Domain.Helpers;
using MediatR;

namespace FlightManagement.Application.Queries
{
    public class GetPersonQuery : IRequest<Result<PersonResponse>>
    {
        public Guid PersonId { get; set; }
    }
}