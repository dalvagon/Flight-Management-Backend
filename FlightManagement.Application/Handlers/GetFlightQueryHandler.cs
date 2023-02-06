using FlightManagement.Application.Mappers;
using FlightManagement.Application.Queries;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers
{
    public class GetFlightQueryHandler : IRequestHandler<GetFlightQuery, Result<FlightResponse>>
    {
        private readonly IRepository<Flight> _flightRepository;

        public GetFlightQueryHandler(IRepository<Flight> flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<Result<FlightResponse>> Handle(GetFlightQuery request, CancellationToken cancellationToken)
        {
            var result = await _flightRepository.GetAsync(request.FlightId);
            if (result == null)
            {
                return Result<FlightResponse>.Failure("Couldn't find flight");
            }

            var flight = FlightMapper.Mapper.Map<FlightResponse>(result);

            return Result<FlightResponse>.Success(flight);
        }
    }
}