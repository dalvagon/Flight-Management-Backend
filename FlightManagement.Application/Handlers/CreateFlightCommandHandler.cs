using FlightManagement.Application.Commands;
using FlightManagement.Application.Mappers;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers
{
    public class CreateFlightCommandHandler : IRequestHandler<CreateFlightCommand, Result<FlightResponse>>
    {
        private readonly IRepository<Airport> _airportRepository;
        private readonly IRepository<Flight> _flightRepository;

        public CreateFlightCommandHandler(IRepository<Airport> airportRepository, IRepository<Flight> flightRepository)
        {
            _airportRepository = airportRepository;
            _flightRepository = flightRepository;
        }

        public async Task<Result<FlightResponse>> Handle(CreateFlightCommand request,
            CancellationToken cancellationToken)
        {
            var departureAirport = await _airportRepository.GetAsync(request.DepartureAirportId);
            var destinationAirport = await _airportRepository.GetAsync(request.DestinationAirportId);
            if (destinationAirport == null || departureAirport == null)
            {
                return Result<FlightResponse>.Failure("Couldn't find airports");
            }

            var result = Flight
                .Create(
                    request.DepartureDate,
                    request.ArrivalDate,
                    request.PassengerCapacity,
                    request.BaggageWeightCapacity,
                    request.MaxWeightPerBaggage,
                    request.MaxBaggageWeightPerPassenger,
                    request.MaxBaggageWidth,
                    request.MaxBaggageHeight,
                    request.MaxBaggageLength,
                    departureAirport,
                    destinationAirport
                );

            if (result.IsFailure)
            {
                return Result<FlightResponse>.Failure(result.Error!);
            }

            var newFlight = await _flightRepository.AddAsync(result.Entity!);
            _flightRepository.SaveChangesAsync();

            var flight = FlightMapper.Mapper.Map<FlightResponse>(newFlight);

            return Result<FlightResponse>.Success(flight);
        }
    }
}