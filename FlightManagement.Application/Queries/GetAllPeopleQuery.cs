using FlightManagement.Application.Responses;
using FlightManagement.Domain.Helpers;
using MediatR;

namespace FlightManagement.Application.Queries
{
    public class GetAllPeopleQuery : IRequest<Result<IReadOnlyCollection<PersonResponse>>>
    {
    }
}