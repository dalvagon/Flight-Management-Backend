using System.Collections.Immutable;
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
        GetAllAllergiesQueryHandler : IRequestHandler<GetAllAllergiesQuery,
            Result<IReadOnlyCollection<AllergyResponse>>>
    {
        private readonly IRepository<Allergy> _allergyRepository;

        public GetAllAllergiesQueryHandler(IRepository<Allergy> allergyRepository)
        {
            _allergyRepository = allergyRepository;
        }

        public async Task<Result<IReadOnlyCollection<AllergyResponse>>> Handle(GetAllAllergiesQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _allergyRepository.AllAsync();

            var allergies =
                AllergyMapper.Mapper.Map<IReadOnlyCollection<AllergyResponse>>(result);
            allergies = allergies!.OrderBy(c => c.Name).ToImmutableList();

            return Result<IReadOnlyCollection<AllergyResponse>>.Success(allergies);
        }
    }
}