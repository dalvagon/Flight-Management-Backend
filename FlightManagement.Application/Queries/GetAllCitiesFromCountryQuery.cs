using FlightManagement.Application.Responses;
using FlightManagement.Domain.Helpers;
using MediatR;

namespace FlightManagement.Application.Queries
{
    public class GetAllCitiesFromCountryQuery : IRequest<Result<IReadOnlyCollection<CityResponse>>>
    {
        public string CountryName { get; set; }
    }
}