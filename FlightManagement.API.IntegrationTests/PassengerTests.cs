using FlightManagement.API.Controllers;
using FlightManagement.API.Dtos;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace FlightManagement.API.IntegrationTests
{
    public class PassengerTests : BaseIntegrationTests<PassengersController>
    {
        private const string ApiUrl = "/api/v1/passengers";

        [Fact]
        public void When_CreatePassenger_Then_ShouldReturnPassenger()
        {
            var passengerRepositoryMock = new Mock<IRepository<Passenger>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var flightRepositoryMock = new Mock<IRepository<Flight>>();
            var allergyRepositoryMock = new Mock<IRepository<Allergy>>();
            var flight = CreateFlight();
            var person = CreatePersons()[0];
            var baggages = CreateBaggages();

            var passengersController = new PassengersController(passengerRepositoryMock.Object,
                personRepositoryMock.Object, flightRepositoryMock.Object, allergyRepositoryMock.Object);

            flightRepositoryMock.Setup(p => p.Get(It.IsAny<Guid>()))
                .Returns(flight);
            personRepositoryMock.Setup(p => p.Get(It.IsAny<Guid>()))
                .Returns(person);

            var dto = new CreatePassengerDto
            {
                FlightId = flight.Id,
                PersonId = person.Id,
                Weight = 80,
                BaggageDtos = baggages.Select(baggage => new CreateBaggageDto
                {
                    Width = baggage.Width,
                    Height = baggage.Height,
                    Length = baggage.Length,
                    Weight = baggage.Weight
                }).ToList(),
                AllergyIds = new List<Guid>()
            };

            // Act
            var response = passengersController.Create(dto) as ObjectResult;
            var passengerResponse = response.Value as Passenger;

            // Assert
            passengerResponse.Flight.Should().Be(flight);
            passengerResponse.Person.Should().Be(person);
            passengerResponse.Weight.Should().Be(80);
            passengerResponse.Allergies.Should().HaveCount(0);
            passengerResponse.Baggages.Should().HaveCount(2);
        }

        [Fact]
        public void When_CreatePassengerThatCarriesWeightAboveTheLimit_Then_ShouldReturnBadRequest()
        {
            var passengerRepositoryMock = new Mock<IRepository<Passenger>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var flightRepositoryMock = new Mock<IRepository<Flight>>();
            var allergyRepositoryMock = new Mock<IRepository<Allergy>>();
            var flight = CreateFlight();
            var person = CreatePersons()[0];
            var baggages = new List<Baggage>
            {
                new(5, 2, 4, 1),
                new(10, 2, 2.5, 1.5),
                new(6, 1.5, 4.5, 2),
            };

            var passengersController = new PassengersController(passengerRepositoryMock.Object,
                personRepositoryMock.Object, flightRepositoryMock.Object, allergyRepositoryMock.Object);

            flightRepositoryMock.Setup(p => p.Get(It.IsAny<Guid>()))
                .Returns(flight);
            personRepositoryMock.Setup(p => p.Get(It.IsAny<Guid>()))
                .Returns(person);

            var dto = new CreatePassengerDto
            {
                FlightId = flight.Id,
                PersonId = person.Id,
                Weight = 80,
                BaggageDtos = baggages.Select(baggage => new CreateBaggageDto
                {
                    Width = baggage.Width,
                    Height = baggage.Height,
                    Length = baggage.Length,
                    Weight = baggage.Weight
                }).ToList(),
                AllergyIds = new List<Guid>()
            };

            // Act
            var response = passengersController.Create(dto) as ObjectResult;

            // Assert
            response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            response.Value.Should()
                .Be($"Person with id {person.Id} carries weight above the limit 20");
        }

        [Fact]
        public void When_CreatePassengerTwiceForTheSameFlight_Then_ShouldReturnBadRequest()
        {
            var passengerRepositoryMock = new Mock<IRepository<Passenger>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var flightRepositoryMock = new Mock<IRepository<Flight>>();
            var allergyRepositoryMock = new Mock<IRepository<Allergy>>();
            var flight = CreateFlight();
            var person = CreatePersons()[0];
            var baggages = CreateBaggages();

            var passengersController = new PassengersController(passengerRepositoryMock.Object,
                personRepositoryMock.Object, flightRepositoryMock.Object, allergyRepositoryMock.Object);

            flightRepositoryMock.Setup(p => p.Get(It.IsAny<Guid>()))
                .Returns(flight);
            personRepositoryMock.Setup(p => p.Get(It.IsAny<Guid>()))
                .Returns(person);

            var dto = new CreatePassengerDto
            {
                FlightId = flight.Id,
                PersonId = person.Id,
                Weight = 80,
                BaggageDtos = baggages.Select(baggage => new CreateBaggageDto
                {
                    Width = baggage.Width,
                    Height = baggage.Height,
                    Length = baggage.Length,
                    Weight = baggage.Weight
                }).ToList(),
                AllergyIds = new List<Guid>()
            };

            // Act
            passengersController.Create(dto);
            var response = passengersController.Create(dto) as ObjectResult;
            //var passenger = response.Value as Passenger;

            // Assert
            response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            response.Value.Should()
                .Be(
                    $"Person with id {person.Id} is already a passenger in flight with id {flight.Id}");
        }

        [Fact]
        public void When_CreatePassengerWithBaggageWithWeightAboveTheLimit_Then_ShouldReturnBadRequest()
        {
            var passengerRepositoryMock = new Mock<IRepository<Passenger>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var flightRepositoryMock = new Mock<IRepository<Flight>>();
            var allergyRepositoryMock = new Mock<IRepository<Allergy>>();
            var flight = CreateFlight();
            var person = CreatePersons()[0];
            var baggages = new List<Baggage>
            {
                new(4, 2, 4, 2),
                new(11, 2, 2.5, 1.5),
                new(5, 1.5, 4.5, 2),
            };

            var passengersController = new PassengersController(passengerRepositoryMock.Object,
                personRepositoryMock.Object, flightRepositoryMock.Object, allergyRepositoryMock.Object);

            flightRepositoryMock.Setup(p => p.Get(It.IsAny<Guid>()))
                .Returns(flight);
            personRepositoryMock.Setup(p => p.Get(It.IsAny<Guid>()))
                .Returns(person);

            var dto = new CreatePassengerDto
            {
                FlightId = flight.Id,
                PersonId = person.Id,
                Weight = 80,
                BaggageDtos = baggages.Select(baggage => new CreateBaggageDto
                {
                    Width = baggage.Width,
                    Height = baggage.Height,
                    Length = baggage.Length,
                    Weight = baggage.Weight
                }).ToList(),
                AllergyIds = new List<Guid>()
            };

            // Act
            var response = passengersController.Create(dto) as ObjectResult;

            // Assert
            response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            response.Value.Should()
                .Be(
                    $"Person with id {person.Id} carries a baggage with the weight above the limit of {flight.MaxWeightPerBaggage}");
        }

        [Fact]
        public void When_CreatePassengerWithBaggageWithDimensionsAboveTheLimit_Then_ShouldReturnBadRequest()
        {
            var passengerRepositoryMock = new Mock<IRepository<Passenger>>();
            var personRepositoryMock = new Mock<IRepository<Person>>();
            var flightRepositoryMock = new Mock<IRepository<Flight>>();
            var allergyRepositoryMock = new Mock<IRepository<Allergy>>();
            var flight = CreateFlight();
            var person = CreatePersons()[0];
            var baggages = new List<Baggage>
            {
                new(4, 2, 4, 10),
                new(10, 2, 2.5, 1.5),
                new(5, 1.5, 4.5, 2),
            };

            var passengersController = new PassengersController(passengerRepositoryMock.Object,
                personRepositoryMock.Object, flightRepositoryMock.Object, allergyRepositoryMock.Object);

            flightRepositoryMock.Setup(p => p.Get(It.IsAny<Guid>()))
                .Returns(flight);
            personRepositoryMock.Setup(p => p.Get(It.IsAny<Guid>()))
                .Returns(person);

            var dto = new CreatePassengerDto
            {
                FlightId = flight.Id,
                PersonId = person.Id,
                Weight = 80,
                BaggageDtos = baggages.Select(baggage => new CreateBaggageDto
                {
                    Width = baggage.Width,
                    Height = baggage.Height,
                    Length = baggage.Length,
                    Weight = baggage.Weight
                }).ToList(),
                AllergyIds = new List<Guid>()
            };

            // Act
            var response = passengersController.Create(dto) as ObjectResult;

            // Assert
            response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            response.Value.Should()
                .Be(
                    $"Person with id {person.Id} carries a baggage that has dimensions above the limit of {flight.MaxBaggageWidth} - {flight.MaxBaggageHeight} - {flight.MaxBaggageLength}");
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
                    10,
                    20,
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