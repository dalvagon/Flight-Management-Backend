using FlightManagement.Application.Mappers;
using FlightManagement.Application.Queries;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers
{
    public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, Result<PersonResponse>>
    {
        private readonly IRepository<Person> _personRepository;

        public GetPersonQueryHandler(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Result<PersonResponse>> Handle(GetPersonQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _personRepository.GetAsync(request.PersonId);
            if (result == null)
            {
                return Result<PersonResponse>.Failure("Couldn't find person");
            }

            var person = PersonMapper.Mapper.Map<PersonResponse>(result);

            return Result<PersonResponse>.Success(person);
        }
    }
}