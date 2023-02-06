using System.Net;
using System.Net.Http.Json;
using FlightManagement.API.Controllers;
using FlightManagement.Application.Commands;
using FlightManagement.Application.Queries;
using FlightManagement.Domain.Entities;
using FluentAssertions;

namespace FlightManagement.API.IntegrationTests;

public class AddressTest : BaseIntegrationTests<AddressesController>
{
    private const string ApiUrl = "/api/v1/addresses/";

    [Fact]
    public async Task WhenCreateAddress_Then_ShouldReturnAddressAsync()
    {
        // Arrange
        var address = CreateAddress();
        await Context.Countries.AddAsync(address.Country);
        await Context.Cities.AddAsync(address.City);
        await Context.SaveChangesAsync();

        var command = new CreateAddressCommand()
        {
            Number = address.Number,
            Street = address.Street,
            CityId = address.City.Id,
            CountryId = address.Country.Id
        };

        // Act
        var responseMessage = await HttpClient.PostAsJsonAsync(ApiUrl, command);
        var addressesResponseMessage = await HttpClient.GetAsync(ApiUrl);
        var addresses = await addressesResponseMessage.Content.ReadFromJsonAsync<List<Address>>();

        // Assert
        responseMessage.EnsureSuccessStatusCode();
        responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
        addresses?.Count.Should().Be(1);
    }

    [Fact]
    public async Task WhenCreateAddressWithCityThatDoesNotExist_Then_ShouldReturnBadRequest()
    {
        // Arrange
        var address = CreateAddress();
        await Context.Countries.AddAsync(address.Country);
        await Context.Cities.AddAsync(address.City);
        await Context.SaveChangesAsync();

        var command = new CreateAddressCommand()
        {
            Number = address.Number,
            Street = address.Street,
            CityId = Guid.Empty,
            CountryId = address.Country.Id
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiUrl, command);
        var responseMessage = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseMessage.Should().Contain("'City Id' must not be empty.");
    }

    [Fact]
    public async Task WhenCreateAddressWithCountryThatDoesNotExist_Then_ShouldReturnBadRequest()
    {
        // Arrange
        var address = CreateAddress();
        await Context.Countries.AddAsync(address.Country);
        await Context.Cities.AddAsync(address.City);
        await Context.SaveChangesAsync();

        var command = new CreateAddressCommand()
        {
            Number = address.Number,
            Street = address.Street,
            CityId = address.City.Id,
            CountryId = Guid.Empty
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiUrl, command);
        var responseMessage = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseMessage.Should().Contain("'Country Id' must not be empty.");
    }

    [Fact]
    public async Task WhenCreateAddressWithEmptyNumber_Then_ShouldReturnBadRequest()
    {
        // Arrange
        var address = CreateAddress();
        await Context.Countries.AddAsync(address.Country);
        await Context.Cities.AddAsync(address.City);
        await Context.SaveChangesAsync();

        var command = new CreateAddressCommand()
        {
            Number = "",
            Street = address.Street,
            CityId = address.City.Id,
            CountryId = address.Country.Id
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiUrl, command);
        var responseMessage = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseMessage.Should().Contain("'Number' must not be empty.");
    }

    [Fact]
    public async Task WhenDeleteAddress_Then_DeleteAddress()
    {
        // Arrange
        var address = CreateAddress();
        await Context.Addresses.AddAsync(address);
        await Context.SaveChangesAsync();

        // Act
        var deleteAddressResponseMessage = await HttpClient.DeleteAsync(ApiUrl + address.Id);
        var addressesResponseMessage = await HttpClient.GetAsync(ApiUrl);
        var addresses = await addressesResponseMessage.Content.ReadFromJsonAsync<List<Address>>();

        // Assert
        deleteAddressResponseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
        addresses!.Count.Should().Be(0);
    }

    [Fact]
    public async Task WhenCreateAddress_Then_GetAddressShouldReturnAddress()
    {
        // Arrange
        var address = CreateAddress();
        await Context.Countries.AddAsync(address.Country);
        await Context.Cities.AddAsync(address.City);
        await Context.Addresses.AddAsync(address);
        await Context.SaveChangesAsync();

        // Act
        var getAddressResponseMessage = await HttpClient.GetAsync(ApiUrl + address.Id);
        var getAddressResponse = await getAddressResponseMessage.Content.ReadFromJsonAsync<Address>();

        // Assert
        getAddressResponseMessage.EnsureSuccessStatusCode();
        getAddressResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        getAddressResponse?.Number.Should().Be(address.Number);
        getAddressResponse?.Street.Should().Be(address.Street);
        getAddressResponse?.City.Name.Should().Be(address.City.Name);
        getAddressResponse?.Country.Name.Should().Be(address.Country.Name);
    }

    [Fact]
    public async Task WhenGetAddressThatDoesNotExist_Then_ShouldReturnBadRequest()
    {
        // Arrange
        var command = new GetAddressQuery() { AddressId = Guid.Empty };

        // Act 
        var response = await HttpClient.GetAsync(ApiUrl + command.AddressId);
        var responseMessage = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseMessage.Should().Be("Couldn't find address");
    }

    [Fact]
    public async Task WhenDeleteAddressThatDoesNotExists_Then_ShouldReturnBadRequest()
    {
        // Arrange
        var command = new DeleteAddressCommand() { AddressId = Guid.Empty };

        // Act 
        var response = await HttpClient.DeleteAsync(ApiUrl + command.AddressId);
        var responseMessage = await response.Content.ReadAsStringAsync();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseMessage.Should().Be("Couldn't delete address");
    }

    private static Address CreateAddress()
    {
        var country = Country.Create("USA", "US").Entity!;
        var city = City.Create("New York", country).Entity!;

        return Address.Create("1", "Oak Street", city, country).Entity!;
    }
}