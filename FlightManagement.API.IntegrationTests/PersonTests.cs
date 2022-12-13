using System.Net;
using System.Net.Http.Json;
using FlightManagement.API.Controllers;
using FlightManagement.API.Dtos;
using FlightManagement.Domain.Entities;
using FluentAssertions;

namespace FlightManagement.API.IntegrationTests;

public class PersonTests : BaseIntegrationTests<PeopleController>
{
    private const string ApiUrl = "/api/v1/people/";

    [Fact]
    public async Task When_CreatePerson_Then_ShouldReturnPersonAsync()
    {
        // Arrange
        var country = CreateCountry();
        var city = CreateCity();
        await Context.Countries.AddAsync(country);
        await Context.Cities.AddAsync(city);
        await Context.SaveChangesAsync();

        var dto = new CreatePersonDto
        {
            Name = "John",
            Surname = "Doe",
            Gender = "Male",
            AddressDto = new CreateAddressDto
            {
                Number = "100",
                Street = "Oak Street",
                CityId = city.Id,
                CountryId = country.Id
            }
        };

        // Act
        var responseMessage = await HttpClient.PostAsJsonAsync(ApiUrl, dto);
        var response = await responseMessage.Content.ReadFromJsonAsync<Person>();

        // Assert
        responseMessage.EnsureSuccessStatusCode();
        responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
        response?.Name.Should().Be("John");
        response?.Surname.Should().Be("Doe");
    }

    [Fact]
    public async Task When_GetPerson_Then_ShouldReturnPerson()
    {
        // Arrange
        var person = CreatePerson();
        await Context.AddAsync(person);
        await Context.SaveChangesAsync();

        // Act
        var responseMessage = await HttpClient.GetAsync(ApiUrl + $"{person.Id}");
        var response = await responseMessage.Content.ReadFromJsonAsync<Person>();

        // Assert
        person.Name.Should().Be("John");
        person.Surname.Should().Be("Doe");
    }


    [Fact]
    public async Task When_CreatePersonWithInvalidGender_Then_ShouldReturnBadRequest()
    {
        // Arrange
        var country = CreateCountry();
        var city = CreateCity();
        Context.Countries.Add(country);
        Context.Cities.Add(city);
        Context.SaveChanges();

        var dto = new CreatePersonDto
        {
            Name = "John",
            Surname = "Doe",
            Gender = "Male",
            AddressDto = new CreateAddressDto
            {
                Number = "100",
                Street = "Oak Street",
                CityId = city.Id,
                CountryId = country.Id
            }
        };
        dto.Gender = "MALE";

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiUrl, dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        response.Content.ReadAsStringAsync().Result.Should()
            .Be($"The provided gender {dto.Gender} is not one from the values: Male, Female");
    }

    [Fact]
    public async Task When_DeletePassenger_Then_ShouldReturnSuccess()
    {
        // Arrange
        var person = CreatePerson();
        Context.People.Add(person);
        Context.SaveChanges();

        // Act
        var response = await HttpClient.DeleteAsync(ApiUrl + person.Id);

        // Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
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
        return Person.Create("John", "Doe", new DateTime(1985, 11, 9), "Male", CreateAddress()).Entity!;
    }
}