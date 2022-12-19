using FlightManagement.Application.Responses;
using MediatR;

namespace FlightManagement.Application.Queries
{
    public class GetAllCountriesQuery : IRequest<IReadOnlyCollection<CountryResponse>>
    {
    }
}