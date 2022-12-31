using FlightManagement.Application.Mappers;
using FlightManagement.Application.Queries;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers
{
    public class GetAllFlightsByDepartureAndDestinationCitiesAndDepartureDate : IRequestHandler<
        GetAllFlightsByDepartureAndDestinationCitiesAndDepartureDateQuery, Result<IReadOnlyCollection<FlightResponse>>>
    {
        private readonly IRepository<Flight> _flightRepository;

        public GetAllFlightsByDepartureAndDestinationCitiesAndDepartureDate(IRepository<Flight> flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<Result<IReadOnlyCollection<FlightResponse>>> Handle(
            GetAllFlightsByDepartureAndDestinationCitiesAndDepartureDateQuery request,
            CancellationToken cancellationToken)
        {
            var flights = FlightMapper.Mapper.Map<IReadOnlyCollection<FlightResponse>>(
                await _flightRepository.FindAsync(flight =>
                    flight.DepartureAirport.Address.City.Name == request.DepartureCity &&
                    flight.DestinationAirport.Address.City.Name == request.DestinationCity));

            return Result<IReadOnlyCollection<FlightResponse>>.Success(flights);
        }
    }
}