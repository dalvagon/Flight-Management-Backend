using System.Net;
using System.Net.Http.Json;
using FlightManagement.API.Controllers;
using FlightManagement.API.Dtos;
using FlightManagement.Domain.Entities;
using FluentAssertions;

namespace FlightManagement.API.IntegrationTests;

public class AirportTests : BaseIntegrationTests<AirportsController>
{
    private const string ApiUrl = "/api/v1/airports/";

    [Fact]
    public async Task When_CreateAirport_Then_ShouldReturnAirport()
    {
        // Arrange
        var address = CreateAddress();
        Context.Addresses.Add(address);
        Context.SaveChanges();

        var dto = new CreateAirportDto
        {
            Name = "New York Airport",
            Address = new CreateAddressDto()
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
    public async Task When_DeleteAirport_Then_ShouldReturnSuccess()
    {
        // Arrange
        var address = CreateAddress();
        Context.Addresses.Add(address);
        var airport = CreateAirport();
        Context.Airports.Add(airport);
        Context.SaveChanges();

        // Act
        var response = await HttpClient.DeleteAsync(ApiUrl + airport.Id);

        // Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
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