using System.Text.Json.Serialization;
using FlightManagement.Domain.Helpers;

namespace FlightManagement.Domain.Entities;

public class Person
{
    [JsonInclude] public Guid Id { get; private set; }
    [JsonInclude] public byte[] PasswordHash { get; private set; }
    [JsonInclude] public byte[] PasswordSalt { get; private set; }
    [JsonInclude] public string Email { get; private set; }
    [JsonInclude] public string Name { get; private set; }
    [JsonInclude] public string Surname { get; private set; }
    [JsonInclude] public DateTime DateOfBirth { get; private set; }
    [JsonInclude] public Gender Gender { get; private set; }
    [JsonInclude] public Address Address { get; private set; }
    [JsonInclude] public string Role { get; private set; }

    public static Result<Person> Create(
        string name,
        string surname,
        string email,
        byte[] passwordHash,
        byte[] passwordSalt,
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
            Email = email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            DateOfBirth = dateOfBirth,
            Gender = personGender,
            Address = address,
            Role = "User"
        };

        return Result<Person>.Success(person);
    }
}