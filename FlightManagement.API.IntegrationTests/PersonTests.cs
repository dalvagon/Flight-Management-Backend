using System.Net;
using System.Net.Http.Json;
using FlightManagement.API.Controllers;
using FlightManagement.Application.Commands;
using FlightManagement.Application.Queries;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FluentAssertions;

namespace FlightManagement.API.IntegrationTests;

public class PersonTests : BaseIntegrationTests<PeopleController>
{
    private const string ApiUrl = "/api/v1/people/";

    [Fact]
    public async Task WhenCreatePerson_Then_ShouldReturnPersonAsync()
    {
        // Arrange
        var country = CreateCountry();
        var city = CreateCity();
        await Context.Countries.AddAsync(country);
        await Context.Cities.AddAsync(city);
        await Context.SaveChangesAsync();

        var command = new CreatePersonCommand()
        {
            Name = "John",
            Surname = "Doe",
            Gender = "Male",
            Password = "john1234",
            Email = "john.doe@gmail.com",
            DateOfBirth = new DateTime(1997, 2, 23),
            Address = new CreateAddressCommand()
            {
                Number = "100",
                Street = "Oak Street",
                CityId = city.Id,
                CountryId = country.Id
            }
        };

        // Act
        var responseMessage = await HttpClient.PostAsJsonAsync(ApiUrl, command);
        var response = await responseMessage.Content.ReadFromJsonAsync<Person>();

        // Assert
        responseMessage.EnsureSuccessStatusCode();
        responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
        response?.Name.Should().Be("John");
        response?.Surname.Should().Be("Doe");
    }

    [Fact]
    public async Task WhenCreatePersonWithCityThatDoesNotExist_Then_ShouldReturnBadRequest()
    {
        // Arrange
        var country = CreateCountry();
        var city = CreateCity();
        await Context.Countries.AddAsync(country);
        await Context.Cities.AddAsync(city);
        await Context.SaveChangesAsync();

        var command = new CreatePersonCommand()
        {
            Name = "John",
            Surname = "Doe",
            Gender = "Male",
            Password = "john1234",
            Email = "john.doe@gmail.com",
            DateOfBirth = new DateTime(1997, 2, 23),
            Address = new CreateAddressCommand()
            {
                Number = "100",
                Street = "Oak Street",
                CityId = Guid.Empty,
                CountryId = country.Id
            }
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiUrl, command);
        var responseMessage = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseMessage.Should().Contain("'Address City Id' must not be empty.");
    }

    [Fact]
    public async Task WhenCreatePersonWithExistentEmail_Then_ShouldReturnBadRequest()
    {
        // Arrange
        var person = CreatePerson();
        await Context.AddAsync(person);
        await Context.SaveChangesAsync();

        var command = new CreatePersonCommand()
        {
            Name = "John",
            Surname = "Doe",
            Gender = "Male",
            Password = "john1234",
            Email = "john.doe@gmail.com",
            DateOfBirth = new DateTime(1997, 2, 23),
            Address = new CreateAddressCommand()
            {
                Number = "100",
                Street = "Oak Street",
                CityId = person.Address.City.Id,
                CountryId = person.Address.Country.Id
            }
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiUrl, command);
        var responseMessage = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseMessage.Should().Contain("Person with this email already exists");
    }


    [Fact]
    public async Task WhenCreatePersonWithCountryThatDoesNotExist_Then_ShouldReturnBadRequest()
    {
        // Arrange
        var country = CreateCountry();
        var city = CreateCity();
        await Context.Countries.AddAsync(country);
        await Context.Cities.AddAsync(city);
        await Context.SaveChangesAsync();

        var command = new CreatePersonCommand()
        {
            Name = "John",
            Surname = "Doe",
            Gender = "Male",
            Password = "john1234",
            Email = "john.doe@gmail.com",
            DateOfBirth = new DateTime(1997, 2, 23),
            Address = new CreateAddressCommand()
            {
                Number = "100",
                Street = "Oak Street",
                CityId = city.Id,
                CountryId = Guid.Empty
            }
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiUrl, command);
        var responseMessage = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseMessage.Should().Contain("'Address Country Id' must not be empty.");
    }

    [Fact]
    public async Task WhenCreatePersonWithEmptyNumber_Then_ShouldReturnBadRequest()
    {
        // Arrange
        var country = CreateCountry();
        var city = CreateCity();
        await Context.Countries.AddAsync(country);
        await Context.Cities.AddAsync(city);
        await Context.SaveChangesAsync();

        var command = new CreatePersonCommand()
        {
            Name = "John",
            Surname = "Doe",
            Gender = "Male",
            Password = "john1234",
            Email = "john.doe@gmail.com",
            DateOfBirth = new DateTime(1997, 2, 23),
            Address = new CreateAddressCommand()
            {
                Number = "",
                Street = "Oak Street",
                CityId = city.Id,
                CountryId = country.Id
            }
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiUrl, command);
        var responseMessage = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseMessage.Should().Contain("'Address Number' must not be empty.");
    }

    [Fact]
    public async Task WhenGetPerson_Then_ShouldReturnPerson()
    {
        // Arrange
        var person = CreatePerson();
        await Context.AddAsync(person);
        await Context.SaveChangesAsync();

        // Act
        var responseMessage = await HttpClient.GetAsync(ApiUrl + $"{person.Id}");
        var content = await responseMessage.Content.ReadFromJsonAsync<Person>();

        // Assert
        content!.Name.Should().Be("John");
        content.Surname.Should().Be("Doe");
    }

    [Fact]
    public async Task WhenGetPersonThatDoesNotExist_Then_ShouldReturnBadRequest()
    {
        // Arrange
        var command = new GetPersonQuery() { PersonId = Guid.Empty };

        // Act
        var response = await HttpClient.GetAsync(ApiUrl + $"{command.PersonId}");
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        content.Should().Be("Couldn't find person");
    }


    [Fact]
    public async Task WhenCreatePersonWithInvalidGender_Then_ShouldReturnBadRequest()
    {
        // Arrange
        var country = CreateCountry();
        var city = CreateCity();
        await Context.Countries.AddAsync(country);
        await Context.Cities.AddAsync(city);
        await Context.SaveChangesAsync();

        var command = new CreatePersonCommand()
        {
            Name = "John",
            Surname = "Doe",
            Gender = "Male",
            Password = "john1234",
            Email = "john.doe@gmail.com",
            DateOfBirth = new DateTime(1984, 2, 21),
            Address = new CreateAddressCommand()
            {
                Number = "100",
                Street = "Oak Street",
                CityId = city.Id,
                CountryId = country.Id
            }
        };
        command.Gender = "MALE";

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiUrl, command);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        response.Content.ReadAsStringAsync().Result.Should()
            .Be($"The provided gender {command.Gender} is not one from the values: Male, Female");
    }

    [Fact]
    public async Task WhenGetPassengers_Then_ShouldReturnPassengers()
    {
        // Arrange
        var person = CreatePerson();
        await Context.AddAsync(person);
        await Context.SaveChangesAsync();

        // Act
        var response = await HttpClient.GetAsync(ApiUrl);
        var content = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<PersonResponse>>();

        // Assert
        content.Count.Should().Be(1);
    }

    [Fact]
    public async Task WhenDeletePassenger_Then_ShouldReturnSuccess()
    {
        // Arrange
        var person = CreatePerson();
        await Context.People.AddAsync(person);
        await Context.SaveChangesAsync();

        // Act
        var response = await HttpClient.DeleteAsync(ApiUrl + person.Id);

        // Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task WhenDeletePersonThatDoesNotExists_Then_ShouldReturnBadRequest()
    {
        // Arrange
        var command = new DeletePersonCommand() { PersonId = Guid.Empty };

        // Act 
        var response = await HttpClient.DeleteAsync(ApiUrl + command.PersonId);
        var responseMessage = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseMessage.Should().Be("Couldn't delete person");
    }

    private static Address CreateAddress()
    {
        var country = CreateCountry();
        var city = CreateCity();

        return Address.Create("1", "Oak Street", city, country).Entity!;
    }

    private static Country CreateCountry()
    {
        return Country.Create("USA", "US").Entity!;
    }

    private static City CreateCity()
    {
        var country = CreateCountry();
        return City.Create("New York", country).Entity!;
    }

    private static Person CreatePerson()
    {
        return Person.Create("John", "Doe", "john.doe@gmail.com", Array.Empty<byte>(), Array.Empty<byte>(),
            new DateTime(1985, 11, 9), "Male", CreateAddress()).Entity!;
    }
}