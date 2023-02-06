using System.Net;
using System.Net.Http.Json;
using FlightManagement.API.Controllers;
using FlightManagement.Application.Commands;
using FlightManagement.Application.Queries;
using FlightManagement.Domain.Entities;
using FluentAssertions;

namespace FlightManagement.API.IntegrationTests;

public class AirportTests : BaseIntegrationTests<AirportsController>
{
    private const string ApiUrl = "/api/v1/airports/";

    [Fact]
    public async Task WhenCreateAirport_Then_ShouldReturnAirport()
    {
        // Arrange
        var address = CreateAddress();
        await Context.Addresses.AddAsync(address);
        await Context.SaveChangesAsync();

        var dto = new CreateAirportCommand()
        {
            Name = "New York Airport",
            Address = new CreateAddressCommand()
            {
                CityId = address.City.Id,
                CountryId = address.Country.Id,
                Number = address.Number,
                Street = address.Street
            }
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiUrl, dto);

        // Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task WhenCreateAirportWithCityThatDoesNotExist_Then_ShouldReturnBadRequest()
    {
        // Arrange
        var address = CreateAddress();
        await Context.Addresses.AddAsync(address);
        await Context.SaveChangesAsync();

        var dto = new CreateAirportCommand()
        {
            Name = "New York Airport",
            Address = new CreateAddressCommand()
            {
                CityId = Guid.Empty,
                CountryId = address.Country.Id,
                Number = address.Number,
                Street = address.Street
            }
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiUrl, dto);
        var responseMessage = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseMessage.Should().Contain("'Address City Id' must not be empty.");
    }

    [Fact]
    public async Task WhenCreateAirportWithCountryThatDoesNotExist_Then_ShouldReturnBadRequest()
    {
        // Arrange
        var address = CreateAddress();
        await Context.Addresses.AddAsync(address);
        await Context.SaveChangesAsync();

        var dto = new CreateAirportCommand()
        {
            Name = "New York Airport",
            Address = new CreateAddressCommand()
            {
                CityId = address.City.Id,
                CountryId = Guid.Empty,
                Number = address.Number,
                Street = address.Street
            }
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiUrl, dto);
        var responseMessage = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseMessage.Should().Contain("'Address Country Id' must not be empty.");
    }

    [Fact]
    public async Task WhenCreateAirportWithBadAddress_Then_ShouldReturnBadRequest()
    {
        // Arrange
        var address = CreateAddress();
        await Context.Addresses.AddAsync(address);
        await Context.SaveChangesAsync();

        var dto = new CreateAirportCommand()
        {
            Name = "New York Airport",
            Address = new CreateAddressCommand()
            {
                CityId = address.City.Id,
                CountryId = address.Country.Id,
                Number = "",
                Street = address.Street
            }
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiUrl, dto);
        var responseMessage = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseMessage.Should().Contain("'Address Number' must not be empty.");
    }

    [Fact]
    public async Task WhenDeleteAirport_Then_ShouldReturnSuccess()
    {
        // Arrange
        var address = CreateAddress();
        await Context.Addresses.AddAsync(address);
        var airport = CreateAirport();
        await Context.Airports.AddAsync(airport);
        await Context.SaveChangesAsync();

        // Act
        var response = await HttpClient.DeleteAsync(ApiUrl + airport.Id);

        // Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task WhenGetAirportThatDoesNotExist_Then_ShouldReturnBadRequest()
    {
        // Arrange
        var command = new GetAirportQuery() { AirportId = Guid.Empty };

        // Act 
        var response = await HttpClient.GetAsync(ApiUrl + command.AirportId);
        var responseMessage = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseMessage.Should().Be("Couldn't find airport");
    }

    [Fact]
    public async Task WhenDeleteAirportThatDoesNotExists_Then_ShouldReturnBadRequest()
    {
        // Arrange
        var command = new DeleteAirportCommand() { AirportId = Guid.Empty };

        // Act 
        var response = await HttpClient.DeleteAsync(ApiUrl + command.AirportId);
        var responseMessage = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseMessage.Should().Be("Couldn't delete airport");
    }

    [Fact]
    public async Task WhenGetAirports_Then_ShouldReturnAirports()
    {
        // Arrange
        var airport = CreateAirport();
        await Context.Airports.AddAsync(airport);
        await Context.SaveChangesAsync();

        // Act
        var response = await HttpClient.GetAsync(ApiUrl);
        var content = await response.Content.ReadFromJsonAsync<List<Airport>>();

        // Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content!.Count.Should().Be(1);
    }

    [Fact]
    public async Task WhenGetAirport_Then_ShouldReturnAirport()
    {
        // Arrange
        var airport = CreateAirport();
        await Context.Airports.AddAsync(airport);
        await Context.SaveChangesAsync();

        // Act
        var response = await HttpClient.GetAsync(ApiUrl + airport.Id);
        var content = await response.Content.ReadFromJsonAsync<Airport>();

        // Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content!.Name.Should().Be(airport.Name);
        content.Address.Country.Name.Should().Be(airport.Address.Country.Name);
        content.Address.City.Name.Should().Be(airport.Address.City.Name);
    }

    [Fact]
    public async Task WhenGetAirportByCity_Then_ShouldReturnAirports()
    {
        // Arrange
        var airport = CreateAirport();
        await Context.Airports.AddAsync(airport);
        await Context.SaveChangesAsync();

        // Act
        var response = await HttpClient.GetAsync(ApiUrl + $"byCity?cityName={airport.Address.City.Name}");
        var content = await response.Content.ReadFromJsonAsync<List<Airport>>();

        // Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        content!.Count.Should().Be(1);
    }

    private static Airport CreateAirport()
    {
        return Airport.Create("Airport", CreateAddress()).Entity!;
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
}