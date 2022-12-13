using System.Text.Json.Serialization;
using FlightManagement.Domain.Helpers;

namespace FlightManagement.Domain.Entities;

public class Administrator
{
    [JsonInclude] public Guid Id { get; private set; }
    [JsonInclude] public Company Company { get; private set; }
    [JsonInclude] public Person Person { get; private set; }

    public static Result<Administrator> Create(Company company, Person person)
    {
        return Result<Administrator>.Success(
            new Administrator
            {
                Id = Guid.NewGuid(),
                Company = company,
                Person = person
            }
        );
    }
}