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
        GetAllPeopleQueryHandler : IRequestHandler<GetAllPeopleQuery, Result<IReadOnlyCollection<PersonResponse>>>
    {
        private readonly IRepository<Person> _personRepository;

        public GetAllPeopleQueryHandler(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Result<IReadOnlyCollection<PersonResponse>>> Handle(GetAllPeopleQuery request,
            CancellationToken cancellationToken)
        {
            var people =
                PersonMapper.Mapper.Map<IReadOnlyCollection<PersonResponse>>(await _personRepository.AllAsync());

            return Result<IReadOnlyCollection<PersonResponse>>.Success(people);
        }
    }
}