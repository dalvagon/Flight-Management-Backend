using FlightManagement.Application.Responses;
using FlightManagement.Domain.Helpers;
using MediatR;

namespace FlightManagement.Application.Queries
{
    public class GetAllCountriesQuery : IRequest<Result<IReadOnlyCollection<CountryResponse>>>
    {
    }
}