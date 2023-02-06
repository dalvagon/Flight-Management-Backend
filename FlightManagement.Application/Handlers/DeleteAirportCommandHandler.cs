using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using FlightManagement.Application.Commands;
using FlightManagement.Domain.Helpers;
using MediatR;

namespace FlightManagement.Application.Handlers
{
    public class DeleteAirportCommandHandler : IRequestHandler<DeleteAirportCommand, Result>
    {
        private readonly IRepository<Airport> _airportRepository;

        public DeleteAirportCommandHandler(IRepository<Airport> airportRepository)
        {
            _airportRepository = airportRepository;
        }

        public async Task<Result> Handle(DeleteAirportCommand request, CancellationToken cancellationToken)
        {
            var airport = await _airportRepository.GetAsync(request.AirportId);
            if (airport == null)
            {
                return Result.Failure("Couldn't delete airport");
            }

            _airportRepository.Delete(airport);
            _airportRepository.SaveChangesAsync();

            return Result.Success();
        }
    }
}