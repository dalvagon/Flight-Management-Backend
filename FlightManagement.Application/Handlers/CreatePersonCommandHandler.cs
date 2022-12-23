﻿using FlightManagement.Application.Commands;
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
            var city = await _cityRepository.GetAsync(request.Address.CityId);
            var country = await _countryRepository.GetAsync(request.Address.CountryId);
            if (city == null)
            {
                return Result<PersonResponse>.Failure("Couldn't find city");
            }

            if (country == null)
            {
                return Result<PersonResponse>.Failure("Couldn't find country");
            }

            var addressResult = Address.Create(request.Address.Number, request.Address.Street, city, country);
            if (addressResult.IsFailure)
            {
                return Result<PersonResponse>.Failure(addressResult.Error!);
            }

            var personResponse = Person.Create(request.Name, request.Surname, request.DateOfBirth, request.Gender,
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
    }
}