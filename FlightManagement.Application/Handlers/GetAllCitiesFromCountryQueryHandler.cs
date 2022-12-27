using System.Collections.Immutable;
using FlightManagement.Application.Mappers;
using FlightManagement.Application.Queries;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers
{
    public class GetAllCitiesFromCountryQueryHandler : IRequestHandler<GetAllCitiesFromCountryQuery,
        Result<IReadOnlyCollection<CityResponse>>>
    {
        private readonly IRepository<City> _cityRepository;

        public GetAllCitiesFromCountryQueryHandler(IRepository<City> cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<Result<IReadOnlyCollection<CityResponse>>> Handle(GetAllCitiesFromCountryQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _cityRepository.FindAsync(c => c.Country.Name == request.CountryName);
            if (!result.Any())
            {
                return Result<IReadOnlyCollection<CityResponse>>.Failure("Couldn't get cities");
            }

            var cities = CityMapper.Mapper.Map<IReadOnlyCollection<CityResponse>>(result);
            cities = cities!.OrderBy(c => c.Name).ToImmutableList();

            return Result<IReadOnlyCollection<CityResponse>>.Success(cities);
        }
    }
}