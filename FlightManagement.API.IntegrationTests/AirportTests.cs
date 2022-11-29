using FlightManagement.API.Controllers;
using FlightManagement.API.Dtos;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FlightManagement.API.IntegrationTests
{
    public class AirportTests
    {
        [Fact]
        public void When_CreateFlight_Then_ShouldReturnFlight()
        {
            // Arrange
            var airportRepositoryMock = new Mock<IRepository<Airport>>();
            var addressRepositoryMock = new Mock<IRepository<Address>>();
            var address = CreateAddress();
            addressRepositoryMock.Setup(p => p.Get(It.IsAny<Guid>()))
                .Returns(address);
            var airportController = new AirportsController(airportRepositoryMock.Object, addressRepositoryMock.Object);
            var dto = new CreateAirportDto()
            {
                Name = "NY Airport",
                AddressId = address.Id,
                City = address.City
            };

            // Act
            var response = airportController.Create(dto) as ObjectResult;
            var airportResponse = response.Value as Airport;

            // Assert
            airportResponse.Address.Should().Be(address);
        }

        private Airport CreateAirport()
        {
            return Airport.Create("Airport", CreateAddress(), "City").Entity;
        }

        private Address CreateAddress()
        {
            return new Address("1", "Oak Street", "New York", "USA");
        }
    }
}