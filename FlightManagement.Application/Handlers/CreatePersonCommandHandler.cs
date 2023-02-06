using System.Security.Cryptography;
using FlightManagement.Application.Commands;
using FlightManagement.Application.Mappers;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FlightManagement.Infrastructure.Generics;
using MediatR;

namespace FlightManagement.Application.Handlers
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Result<PersonResponse>>
    {
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Person> _personRepository;

        public CreatePersonCommandHandler(IRepository<Country> countryRepository, IRepository<City> cityRepository,
            IRepository<Person> personRepository)
        {
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _personRepository = personRepository;
        }

        public async Task<Result<PersonResponse>> Handle(CreatePersonCommand request,
            CancellationToken cancellationToken)
        {
            var persons = await _personRepository.FindAsync(p => p.Email == request.Email);
            var verifyPerson = persons.FirstOrDefault();
            if (verifyPerson != null)
            {
                return Result<PersonResponse>.Failure("Person with this email already exists");
            }

            var city = await _cityRepository.GetAsync(request.Address.CityId);
            var country = await _countryRepository.GetAsync(request.Address.CountryId);

            var addressResult = Address.Create(request.Address.Number, request.Address.Street, city, country);

            CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

            var personResponse = Person.Create(request.Name, request.Surname, request.Email, passwordHash, passwordSalt,
                request.DateOfBirth, request.Gender,
                addressResult.Entity!);
            if (personResponse.IsFailure)
            {
                return Result<PersonResponse>.Failure(personResponse.Error!);
            }

            var newPerson = await _personRepository.AddAsync(personResponse.Entity!);
            _personRepository.SaveChangesAsync();

            var person = PersonMapper.Mapper.Map<PersonResponse>(newPerson);

            return Result<PersonResponse>.Success(person);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}