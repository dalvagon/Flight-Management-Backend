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
        GetAllBaggagesQueryHandler : IRequestHandler<GetAllBaggagesQuery, Result<IReadOnlyCollection<BaggageResponse>>>
    {
        private readonly IRepository<Baggage> _baggageRepository;

        public GetAllBaggagesQueryHandler(IRepository<Baggage> baggageRepository)
        {
            _baggageRepository = baggageRepository;
        }

        public async Task<Result<IReadOnlyCollection<BaggageResponse>>> Handle(GetAllBaggagesQuery request,
            CancellationToken cancellationToken)
        {
            var baggages =
                BaggageMapper.Mapper.Map<IReadOnlyCollection<BaggageResponse>>(await _baggageRepository.AllAsync());

            return Result<IReadOnlyCollection<BaggageResponse>>.Success(baggages);
        }
    }
}