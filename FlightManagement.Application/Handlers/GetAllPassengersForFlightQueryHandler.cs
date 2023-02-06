using FlightManagement.Application.Mappers;
using FlightManagement.Application.Queries;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers
{
    public class GetAllPassengersForFlightQueryHandler : IRequestHandler<GetAllPassengersForFlightQuery,
        Result<IReadOnlyCollection<PassengerResponse>>>
    {
        private readonly IRepository<Passenger> _passengerRepository;

        public GetAllPassengersForFlightQueryHandler(IRepository<Passenger> passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }

        public async Task<Result<IReadOnlyCollection<PassengerResponse>>> Handle(GetAllPassengersForFlightQuery request,
            CancellationToken cancellationToken)
        {
            var passengers =
                PassengerMapper.Mapper.Map<IReadOnlyCollection<PassengerResponse>>(
                    await _passengerRepository.FindAsync(p => p.Flight.Id == request.FlightId));

            return Result<IReadOnlyCollection<PassengerResponse>>.Success(passengers);
        }
    }
}