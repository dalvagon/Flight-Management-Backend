using FlightManagement.Application.Mappers;
using FlightManagement.Application.Queries;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers
{
    public class GetPassengerQueryHandler : IRequestHandler<GetPassengerQuery, Result<PassengerResponse>>
    {
        private readonly IRepository<Passenger> _passengerRepository;

        public GetPassengerQueryHandler(IRepository<Passenger> passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }

        public async Task<Result<PassengerResponse>> Handle(GetPassengerQuery request,
            CancellationToken cancellationToken)
        {
            var result =
                PassengerMapper.Mapper.Map<PassengerResponse>(
                    await _passengerRepository.GetAsync(request.PassengerId));
            if (result == null)
            {
                return Result<PassengerResponse>.Failure("Couldn't find passenger");
            }

            var passenger = PassengerMapper.Mapper.Map<PassengerResponse>(result);

            return Result<PassengerResponse>.Success(passenger);
        }
    }
}