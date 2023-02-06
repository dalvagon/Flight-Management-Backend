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
        GetAllFlightsQueryHandler : IRequestHandler<GetAllFlightsQuery, Result<IReadOnlyCollection<FlightResponse>>>
    {
        private readonly IRepository<Flight> _flightRepository;

        public GetAllFlightsQueryHandler(IRepository<Flight> flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<Result<IReadOnlyCollection<FlightResponse>>> Handle(GetAllFlightsQuery request,
            CancellationToken cancellationToken)
        {
            var flights =
                FlightMapper.Mapper.Map<IReadOnlyCollection<FlightResponse>>(await _flightRepository.AllAsync());

            return Result<IReadOnlyCollection<FlightResponse>>.Success(flights);
        }
    }
}