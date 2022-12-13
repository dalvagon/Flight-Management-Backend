using FlightManagement.Application.Commands;
using FlightManagement.Application.Mappers;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers;

public class CreateFlightCommandHandler : IRequestHandler<CreateFlightCommand, FlightResponse>
{
    private readonly IRepository<Flight> _flightRepository;

    public CreateFlightCommandHandler(IRepository<Flight> flightRepository)
    {
        _flightRepository = flightRepository;
    }

    public async Task<FlightResponse> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
    {
        var flight = FlightMapper.Mapper.Map<Flight>(request);
        if (flight == null) throw new ApplicationException("Issue with mapper");

        var newFlight = await _flightRepository.AddAsync(flight);
        _flightRepository.SaveChangesAsync();

        return FlightMapper.Mapper.Map<FlightResponse>(newFlight);
    }
}