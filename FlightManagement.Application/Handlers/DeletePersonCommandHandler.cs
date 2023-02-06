using FlightManagement.Application.Commands;
using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Result>
    {
        private readonly IRepository<Person> _personRepository;

        public DeletePersonCommandHandler(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Result> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetAsync(request.PersonId);
            if (person == null)
            {
                return Result.Failure("Couldn't delete person");
            }

            _personRepository.Delete(person);
            _personRepository.SaveChangesAsync();

            return Result.Success();
        }
    }
}