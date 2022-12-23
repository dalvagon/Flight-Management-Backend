using FlightManagement.Application.Commands;
using FlightManagement.Application.Mappers;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers
{
    public class CreateAirportCommandHandler : IRequestHandler<CreateAirportCommand, Result<AirportResponse>>
    {
        private readonly IRepository<Airport> _airportRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<City> _cityRepository;

        public CreateAirportCommandHandler(IRepository<Airport> airportRepository,
            IRepository<Country> countryRepository, IRepository<City> cityRepository)
        {
            _airportRepository = airportRepository;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
        }

        public async Task<Result<AirportResponse>> Handle(CreateAirportCommand request,
            CancellationToken cancellationToken)
        {
            var city = await _cityRepository.GetAsync(request.Address.CityId);
            var country = await _countryRepository.GetAsync(request.Address.CountryId);
            if (city == null)
            {
                return Result<AirportResponse>.Failure("Couldn't find city");
            }

            if (country == null)
            {
                return Result<AirportResponse>.Failure("Couldn't find country");
            }

            var address = Address.Create(request.Address.Number, request.Address.Street, city, country);
            if (address.IsFailure)
            {
                return Result<AirportResponse>.Failure(address.Error!);
            }

            var result = Airport.Create(request.Name, address.Entity!);
            if (result.IsFailure)
            {
                return Result<AirportResponse>.Failure(result.Error!);
            }

            var newAirport = await _airportRepository.AddAsync(result.Entity!);
            _airportRepository.SaveChangesAsync();
            if (newAirport == null)
            {
                return Result<AirportResponse>.Failure("Couldn't save airport");
            }

            var airport = AirportMapper.Mapper.Map<AirportResponse>(newAirport);

            return Result<AirportResponse>.Success(airport);
        }
    }
}