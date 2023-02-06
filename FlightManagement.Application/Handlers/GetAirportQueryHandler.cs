using FlightManagement.Application.Mappers;
using FlightManagement.Application.Queries;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers
{
    public class GetAirportQueryHandler : IRequestHandler<GetAirportQuery, Result<AirportResponse>>
    {
        private readonly IRepository<Airport> _airportRepository;

        public GetAirportQueryHandler(IRepository<Airport> airportRepository)
        {
            _airportRepository = airportRepository;
        }

        public async Task<Result<AirportResponse>> Handle(GetAirportQuery request, CancellationToken cancellationToken)
        {
            var result = await _airportRepository.GetAsync(request.AirportId);
            if (result == null)
            {
                return Result<AirportResponse>.Failure("Couldn't find airport");
            }

            var airport = AirportMapper.Mapper.Map<AirportResponse>(result);

            return Result<AirportResponse>.Success(airport);
        }
    }
}