using FlightManagement.API.Controllers;
using FlightManagement.API.Dtos;
using FlightManagement.Domain.Entities;
using FluentAssertions;
using Moq;
using System.Net;
using System.Net.Http.Json;
using FlightManagement.Infrastructure.Generics;

namespace FlightManagement.API.IntegrationTests
{
    public class Baggage : BaseIntegrationTests<BaggagesController>
    {
        private const string ApiUrl = "/api/v1/baggages";

        [Fact]
        public async Task When_CreateBaggage_Then_ShouldReturnBaggageAsync()
        {
            // Arrange
            var passenger = CreatePassengers()[0];

            var passengerRepositoryMock = new Mock<IRepository<Passenger>>();
            passengerRepositoryMock.Setup(passengerRepository => passengerRepository.Get(It.IsAny<Guid>()))
                .Returns(passenger);

            CreateBaggageDto dto = new CreateBaggageDto
            {
                Weight = 10,
                Width = 2,
                Height = 1,
                Length = 10,
            };

            // Act
            var response = await HttpClient.PostAsJsonAsync(ApiUrl, dto);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        private List<Passenger> CreatePassengers()
        {
            var persons = CreatePersons();
            var flight = CreateFlight();
            var passengers = (
                persons.Select(person => Passenger.Create(person, flight, 20).Entity).ToList()
            );

            return passengers;
        }

        private Flight CreateFlight()
        {
            return Flight
                .Create(
                    new DateTime(2022, 11, 23, 10, 30, 0),
                    new DateTime(2022, 11, 23, 22, 30, 0),
                    200,
                    1000000,
                    20,
                    40,
                    10,
                    2,
                    5,
                    CreateDepartureAirport(),
                    CreateArrivalAirport()
                )
                .Entity;
        }

        private Airport CreateDepartureAirport()
        {
            var address = CreateAddress1();
            return Airport.Create("Wizz Airport", address, "Bucharest").Entity;
        }

        private Airport CreateArrivalAirport()
        {
            var address = CreateAddress2();
            return Airport.Create("Suceava Airport", address, "Suceava").Entity;
        }

        private Address CreateAddress1()
        {
            return new Address("100", "Carol 1", "Bucharest", "Romania");
        }

        private Address CreateAddress2()
        {
            return new Address("2087", "Mihai Eminescu", "Suceava", "Romania");
        }

        private List<Person> CreatePersons()
        {
            return new List<Person>()
            {
                Person.Create("John", "Doe", new DateTime(1998, 10, 11), "Male").Entity,
                Person.Create("Al", "Pacino", new DateTime(2000, 1, 24), "Male").Entity,
                Person.Create("Ina", "Jackson", new DateTime(1979, 5, 1), "Female").Entity,
            };
        }
    }
}