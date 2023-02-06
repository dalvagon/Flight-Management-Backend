using FlightManagement.Application.Mappers;
using FlightManagement.Application.Queries;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FlightManagement.Infrastructure.Generics;
using MediatR;
using System.Collections.Immutable;

namespace FlightManagement.Application.Handlers
{
    public class
        GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery,
            Result<IReadOnlyCollection<CountryResponse>>>
    {
        private readonly IRepository<Country> _countryRepository;

        public GetAllCountriesQueryHandler(IRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<Result<IReadOnlyCollection<CountryResponse>>> Handle(GetAllCountriesQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _countryRepository.AllAsync();
            if (!result.Any())
            {
                return Result<IReadOnlyCollection<CountryResponse>>.Failure("Couldn't get countries");
            }

            var countries =
                CountryMapper.Mapper.Map<IReadOnlyCollection<CountryResponse>>(result);
            countries = countries!.OrderBy(c => c.Name).ToImmutableList();

            return Result<IReadOnlyCollection<CountryResponse>>.Success(countries);
        }
    }
}