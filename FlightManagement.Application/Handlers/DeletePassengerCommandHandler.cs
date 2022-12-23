using FlightManagement.Application.Commands;
using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers
{
    public class DeletePassengerCommandHandler : IRequestHandler<DeletePassengerCommand, Result>
    {
        private readonly IRepository<Passenger> _passengerRepository;

        public DeletePassengerCommandHandler(IRepository<Passenger> passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }

        public async Task<Result> Handle(DeletePassengerCommand request, CancellationToken cancellationToken)
        {
            var passenger = await _passengerRepository.GetAsync(request.PassengerId);
            if (passenger == null)
            {
                return Result.Failure("Couldn't delete passenger");
            }

            _passengerRepository.Delete(passenger);
            _passengerRepository.SaveChangesAsync();

            return Result.Success();
        }
    }
}