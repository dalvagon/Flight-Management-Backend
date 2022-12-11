using System.Net;
using System.Net.Http.Json;
using FlightManagement.API.Controllers;
using FlightManagement.API.Dtos;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FlightManagement.API.IntegrationTests
{
    public class AirportTests : BaseIntegrationTests<AirportsController>
    {
        private const string ApiUrl = "/api/v1/airports/";

        [Fact]
        public async Task When_CreateMockAirport_Then_ShouldReturnAirport()
        {
            // Arrange
            var airportRepositoryMock = new Mock<IRepository<Airport>>();
            var addressRepositoryMock = new Mock<IRepository<Address>>();
            var address = CreateAddress();
            addressRepositoryMock.Setup(p => p.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(address);
            var airportController = new AirportsController(airportRepositoryMock.Object, addressRepositoryMock.Object);
            var dto = new CreateAirportDto()
            {
                Name = "NY Airport",
                AddressId = address.Id,
            };

            // Act
            var response = await airportController.Create(dto) as ObjectResult;
            var airportResponse = response.Value as Airport;

            // Assert
            airportResponse.Address.Should().Be(address);
        }

        [Fact]
        public async Task When_CreateAirport_Then_ShouldReturnAirport()
        {
            // Arrange
            var address = CreateAddress();
            Context.Addresses.Add(address);
            Context.SaveChanges();

            var dto = new CreateAirportDto()
            {
                Name = "New York Airport",
                AddressId = address.Id,
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

        private Airport CreateAirport()
        {
            return Airport.Create("Airport", CreateAddress()).Entity;
        }

        private Address CreateAddress()
        {
            var country = CreateCountry();
            var city = CreateCity();

            return Address.Create("1", "Oak Street", city, country).Entity;
        }

        private Country CreateCountry()
        {
            return Country.Create("USA", "US").Entity;
        }

        private City CreateCity()
        {
            var country = CreateCountry();
            return City.Create("New York", country).Entity;
        }
    }
}