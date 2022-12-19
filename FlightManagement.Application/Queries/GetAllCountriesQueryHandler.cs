using FlightManagement.Application.Mappers;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Queries
{
    public class
        GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, IReadOnlyCollection<CountryResponse>>
    {
        private readonly IRepository<Country> _countryRepository;

        public GetAllCountriesQueryHandler(IRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<IReadOnlyCollection<CountryResponse>> Handle(GetAllCountriesQuery request,
            CancellationToken cancellationToken)
        {
            var countries =
                CountryMapper.Mapper.Map<IReadOnlyCollection<CountryResponse>>(await _countryRepository.AllAsync());

            return countries;
        }
    }
}