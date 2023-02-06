using FlightManagement.Application.Mappers;
using FlightManagement.Application.Queries;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers
{
    public class
        GetAllAirportsQueryHandler : IRequestHandler<GetAllAirportsQuery, Result<IReadOnlyCollection<AirportResponse>>>
    {
        private readonly IRepository<Airport> _airportRepository;

        public GetAllAirportsQueryHandler(IRepository<Airport> airportRepository)
        {
            _airportRepository = airportRepository;
        }

        public async Task<Result<IReadOnlyCollection<AirportResponse>>> Handle(GetAllAirportsQuery request,
            CancellationToken cancellationToken)
        {
            var airports =
                AirportMapper.Mapper.Map<IReadOnlyCollection<AirportResponse>>(await _airportRepository.AllAsync());

            return Result<IReadOnlyCollection<AirportResponse>>.Success(airports);
        }
    }
}