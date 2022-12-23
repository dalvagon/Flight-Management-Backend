using FlightManagement.Application.Mappers;
using FlightManagement.Application.Queries;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers
{
    public class GetAllAirportsByCityQueryHandler : IRequestHandler<GetAllAirportsByCityQuery,
        Result<IReadOnlyCollection<AirportResponse>>>
    {
        private readonly IRepository<Airport> _airportRepository;

        public GetAllAirportsByCityQueryHandler(IRepository<Airport> airportRepository)
        {
            _airportRepository = airportRepository;
        }

        public async Task<Result<IReadOnlyCollection<AirportResponse>>> Handle(GetAllAirportsByCityQuery request,
            CancellationToken cancellationToken)
        {
            var airports =
                AirportMapper.Mapper.Map<IReadOnlyCollection<AirportResponse>>(
                    await _airportRepository.FindAsync(a => a.Address.City.Name == request.CityName));

            return Result<IReadOnlyCollection<AirportResponse>>.Success(airports);
        }
    }
}