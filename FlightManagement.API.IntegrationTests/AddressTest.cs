using FlightManagement.API.Controllers;
using System.Net.Http.Json;
using System.Net;
using FlightManagement.API.Dtos;
using FluentAssertions;
using FlightManagement.Domain.Entities;

namespace FlightManagement.API.IntegrationTests
{
    public class AddressTest : BaseIntegrationTests<AddressesController>
    {
        private const string ApiUrl = "/api/v1/addresses/";

        [Fact]
        public async Task WhenCreateAddress_Then_ShouldReturnAddressAsync()
        {
            // Arrange
            var address = CreateAddress();
            var dto = new CreateAddressDto()
            {
                Number = address.Number,
                Street = address.Street,
                City = address.City,
                Country = address.Country,
            };

            // Act
            var responseMessage = await HttpClient.PostAsJsonAsync(ApiUrl, dto);
            var addressesResponseMessage = await HttpClient.GetAsync(ApiUrl);
            var addresses = await addressesResponseMessage.Content.ReadFromJsonAsync<List<Address>>();

            // Assert
            responseMessage.EnsureSuccessStatusCode();
            responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
            addresses.Count.Should().Be(1);
        }

        [Fact]
        public async Task WhenDeleteAddress_Then_DeleteAddress()
        {
            // Arrange
            var address = CreateAddress();
            Context.Addresses.Add(address);
            Context.SaveChanges();

            // Act
            var deleteAddressResponseMessage = await HttpClient.DeleteAsync(ApiUrl + address.Id);
            var addressesResponseMessage = await HttpClient.GetAsync(ApiUrl);
            var addresses = await addressesResponseMessage.Content.ReadFromJsonAsync<List<Address>>();

            // Assert
            deleteAddressResponseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
            addresses.Count.Should().Be(0);
        }

        [Fact]
        public async Task WhenCreateAddress_Then_GetAddressShouldReturnAddress()
        {
            // Arrange
            var address = CreateAddress();
            Context.Addresses.Add(address);
            Context.SaveChanges();

            // Act
            var getAddressResponseMessage = await HttpClient.GetAsync(ApiUrl + address.Id);
            var getAddressResponse = await getAddressResponseMessage.Content.ReadFromJsonAsync<Address>();

            // Assert
            getAddressResponseMessage.EnsureSuccessStatusCode();
            getAddressResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            getAddressResponse.Number.Should().Be(address.Number);
            getAddressResponse.Street.Should().Be(address.Street);
            getAddressResponse.City.Should().Be(address.City);
            getAddressResponse.Country.Should().Be(address.Country);
        }


        private Address CreateAddress()
        {
            return new Address("1", "Oak Street", "New York", "USA");
        }
    }
}