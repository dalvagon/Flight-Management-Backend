using System.Net;
using FlightManagement.API.Controllers;
using FlightManagement.API.Dtos;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net.Http.Json;

namespace FlightManagement.API.IntegrationTests
{
    public class FlightTests : BaseIntegrationTests<FlightsController>
    {
        private const string ApiUrl = "/api/v1/flights";

        [Fact]
        public void When_CreateFlight_Then_ShouldReturnFlight()
        {
            // Arrange
            var airportRepositoryMock = new Mock<IRepository<Airport>>();
            var flightRepositoryMock = new Mock<IRepository<Flight>>();
            var airport = CreateDepartureAirport();
            airportRepositoryMock.Setup(p => p.Get(It.IsAny<Guid>()))
                .Returns(airport);
            var flightsController = new FlightsController(flightRepositoryMock.Object, airportRepositoryMock.Object);
            var flight = CreateFlight();
            var dto = new CreateFlightDto
            {
                DepartureDate = flight.DepartureDate,
                ArrivalDate = flight.ArrivalDate,
                PassengerCapacity = flight.PassengerCapacity,
                BaggageWeightCapacity = flight.BaggageWeightCapacity,
                MaxWeightPerBaggage = flight.MaxWeightPerBaggage,
                MaxBaggageWeightPerPassenger = flight.MaxBaggageWeightPerPassenger,
                MaxBaggageWidth = flight.MaxBaggageWidth,
                MaxBaggageHeight = flight.MaxBaggageHeight,
                MaxBaggageLength = flight.MaxBaggageLength,
                DepartureAirportId = flight.DepartureAirport.Id,
                DestinationAirportId = flight.DestinationAirport.Id
            };

            // Act
            var response = flightsController.Create(dto) as ObjectResult;
            var flightResponse = response.Value as Flight;

            // Assert
            flightResponse.DepartureDate.Should().Be(flight.DepartureDate);
            flightResponse.ArrivalDate.Should().Be(flight.ArrivalDate);
            flightResponse.PassengerCapacity.Should().Be(flight.PassengerCapacity);
            flightResponse.BaggageWeightCapacity.Should().Be(flight.BaggageWeightCapacity);
            flightResponse.MaxWeightPerBaggage.Should().Be(flight.MaxWeightPerBaggage);
            flightResponse.MaxBaggageWeightPerPassenger.Should().Be(flightResponse.MaxBaggageWeightPerPassenger);
            flightResponse.MaxBaggageWidth.Should().Be(flight.MaxBaggageWidth);
            flightResponse.MaxBaggageHeight.Should().Be(flight.MaxBaggageHeight);
            flightResponse.MaxBaggageLength.Should().Be(flight.MaxBaggageLength);
            flightResponse.DepartureAirport.Should().Be(airport);
            flightResponse.DestinationAirport.Should().Be(airport);
        }

        [Fact]
        public async Task When_CreateFlightWithTheArrivalDatePastTheDepartureDate_Then_ShouldReturnBadRequestAsync()
        {
            // Arrange
            var flight = CreateFlight();
            var dto = new CreateFlightDto
            {
                DepartureDate = flight.ArrivalDate,
                ArrivalDate = flight.DepartureDate,
                PassengerCapacity = flight.PassengerCapacity,
                BaggageWeightCapacity = flight.BaggageWeightCapacity,
                MaxWeightPerBaggage = flight.MaxWeightPerBaggage,
                MaxBaggageWeightPerPassenger = flight.MaxBaggageWeightPerPassenger,
                MaxBaggageWidth = flight.MaxBaggageWidth,
                MaxBaggageHeight = flight.MaxBaggageHeight,
                MaxBaggageLength = flight.MaxBaggageLength,
                DepartureAirportId = flight.DepartureAirport.Id,
                DestinationAirportId = flight.DestinationAirport.Id
            };

            // Act
            var response = await HttpClient.PostAsJsonAsync(ApiUrl, dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Content.ReadAsStringAsync().Result.Should().Be(
                $"The arrival date {dto.ArrivalDate} for the flight is past the departure date {dto.DepartureDate}");
        }

        private List<Passenger> CreatePassengers()
        {
            var persons = CreatePersons();
            var flight = CreateFlight();
            var baggages = CreateBaggages();
            var passengers = (
                persons.Select(person => Passenger.Create(person, flight, 70, baggages, null).Entity).ToList()
            );

            return passengers;
        }

        private List<Baggage> CreateBaggages()
        {
            return new List<Baggage>
            {
                new(10, 2, 1.5, 2),
                new(5, 1.5, 4.5, 2),
            };
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
                    2,
                    5,
                    2.5,
                    CreateDepartureAirport(),
                    CreateDestinationAirport()
                )
                .Entity;
        }

        private Airport CreateDepartureAirport()
        {
            var address = CreateAddress1();
            return Airport.Create("Wizz Airport", address, "Bucharest").Entity;
        }

        private Airport CreateDestinationAirport()
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
            var address = CreateAddress1();

            return new List<Person>
            {
                Person.Create("John", "Doe", new DateTime(1998, 10, 11), "Male", address).Entity,
                Person.Create("Al", "Pacino", new DateTime(2000, 1, 24), "Male", address).Entity,
                Person.Create("Ina", "Jackson", new DateTime(1979, 5, 1), "Female", address).Entity,
            };
        }
    }
}