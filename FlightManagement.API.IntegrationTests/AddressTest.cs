using System.Net;
using System.Net.Http.Json;
using FlightManagement.API.Controllers;
using FlightManagement.API.Dtos;
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

        var dto = new CreateAddressDto
        {
            Number = address.Number,
            Street = address.Street,
            CityId = address.City.Id,
            CountryId = address.Country.Id
        };

        // Act
        var responseMessage = await HttpClient.PostAsJsonAsync(ApiUrl, dto);
        var addressesResponseMessage = await HttpClient.GetAsync(ApiUrl);
        var addresses = await addressesResponseMessage.Content.ReadFromJsonAsync<List<Address>>();

        // Assert
        responseMessage.EnsureSuccessStatusCode();
        responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
        addresses?.Count.Should().Be(1);
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


    private static Address CreateAddress()
    {
        var country = Country.Create("USA", "US").Entity!;
        var city = City.Create("New York", country).Entity!;

        return Address.Create("1", "Oak Street", city, country).Entity!;
    }
}