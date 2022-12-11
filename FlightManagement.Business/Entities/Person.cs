﻿using System.Text.Json.Serialization;
using FlightManagement.Domain.Helpers;

namespace FlightManagement.Domain.Entities
{
    public class Person
    {
        [JsonInclude] public Guid Id { get; private set; }
        [JsonInclude] public string Name { get; private set; }
        [JsonInclude] public string Surname { get; private set; }
        [JsonInclude] public DateTime DateOfBirth { get; private set; }
        [JsonInclude] public Gender Gender { get; private set; }
        [JsonInclude] public Address Address { get; private set; }

        public static Result<Person> Create(
            string name,
            string surname,
            DateTime dateOfBirth,
            string gender,
            Address address
        )
        {
            if (!Enum.TryParse<Gender>(gender, out var personGender))
            {
                var expectedGenderValues = Enum.GetNames(typeof(Gender));
                var textExpectedGenderValues = string.Join(", ", expectedGenderValues);

                return Result<Person>.Failure(
                    $"The provided gender {gender} is not one from the values: {textExpectedGenderValues}"
                );
            }

            var person = new Person
            {
                Id = Guid.NewGuid(),
                Name = name,
                Surname = surname,
                DateOfBirth = dateOfBirth,
                Gender = personGender,
                Address = address
            };

            return Result<Person>.Success(person);
        }
    }
}