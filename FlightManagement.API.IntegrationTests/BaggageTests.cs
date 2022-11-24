using FlightManagement.API.Controllers;
using FlightManagement.API.Dtos;
using FlightManagement.Domain.Entities;
using FluentAssertions;
using Moq;
using FlightManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.IntegrationTests
{
    public class BaggageTests : BaseIntegrationTests<BaggagesController>
    {
        private const string ApiUrl = "/api/v1/baggages";

        [Fact]
        public async Task When_CreateBaggage_Then_ShouldReturnBaggageAsync()
        {
            // Arrange
            var passenger = CreatePassengers()[0];

            var baggageRepositoryMock = new Mock<IRepository<Baggage>>();
            var passengerRepositoryMock = new Mock<IRepository<Passenger>>();
            passengerRepositoryMock.Setup(p => p.Get(It.IsAny<Guid>()))
                .Returns(passenger);

            var baggagesController =
                new BaggagesController(baggageRepositoryMock.Object, passengerRepositoryMock.Object);

            CreateBaggageDto dto = new CreateBaggageDto
            {
                Weight = 10,
                Width = 2,
                Height = 1,
                Length = 10,
                PassengerId = passenger.Id
            };

            // Act
            var response = baggagesController.Add(dto) as ObjectResult;
            var baggage = response.Value as Baggage;
            //var config = response.Value as Configuration;

            // Assert
            baggage.Weight.Should().Be(10);
            baggage.Width.Should().Be(2);
            baggage.Height.Should().Be(1);
            baggage.Length.Should().Be(10);
            baggage.Passenger.Should().Be(passenger);
        }

        private List<Passenger> CreatePassengers()
        {
            var persons = CreatePersons();
            var flight = CreateFlight();
            var passengers = (
                persons.Select(person => Passenger.Create(person, flight, 70).Entity).ToList()
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