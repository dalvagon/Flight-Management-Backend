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
        private const string ApiUrl = "/api/v1/addresses";

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
            var response = await HttpClient.PostAsJsonAsync(ApiUrl, dto);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        private Address CreateAddress()
        {
            return new Address("1", "Oak Street", "New York", "USA");
        }
    }
}