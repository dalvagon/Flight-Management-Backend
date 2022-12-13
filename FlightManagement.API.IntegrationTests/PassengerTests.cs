﻿using System.Net;
using System.Net.Http.Json;
using FlightManagement.API.Controllers;
using FlightManagement.API.Dtos;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FlightManagement.API.IntegrationTests;

public class PassengerTests : BaseIntegrationTests<PassengersController>
{
    private const string ApiUrl = "/api/v1/passengers/";

    [Fact]
    public async Task When_CreatePassenger_Then_ShouldReturnPassenger()
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

        flightRepositoryMock.Setup(p => p.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(flight);
        personRepositoryMock.Setup(p => p.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(person);

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
        var response = await passengersController.Create(dto) as ObjectResult;
        var passengerResponse = response!.Value as Passenger;

        // Assert
        passengerResponse!.Flight.Should().Be(flight);
        passengerResponse.Person.Should().Be(person);
        passengerResponse.Weight.Should().Be(80);
        passengerResponse.Allergies.Should().HaveCount(0);
        passengerResponse.Baggages.Should().HaveCount(2);
    }

    [Fact]
    public async Task When_CreatePassengerThatCarriesWeightAboveTheLimit_Then_ShouldReturnBadRequest()
    {
        var passengerRepositoryMock = new Mock<IRepository<Passenger>>();
        var personRepositoryMock = new Mock<IRepository<Person>>();
        var flightRepositoryMock = new Mock<IRepository<Flight>>();
        var allergyRepositoryMock = new Mock<IRepository<Allergy>>();
        var flight = CreateFlight();
        var person = CreatePersons()[0];
        var baggages = new List<Baggage>
        {
            Baggage.Create(5, 2, 4, 1).Entity!,
            Baggage.Create(10, 2, 2.5, 1.5).Entity!,
            Baggage.Create(6, 1.5, 4.5, 2).Entity!
        };

        var passengersController = new PassengersController(passengerRepositoryMock.Object,
            personRepositoryMock.Object, flightRepositoryMock.Object, allergyRepositoryMock.Object);

        flightRepositoryMock.Setup(p => p.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(flight);
        personRepositoryMock.Setup(p => p.GetAsync(It.IsAny<Guid>())).ReturnsAsync(person);

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
        var response = await passengersController.Create(dto) as ObjectResult;

        // Assert
        response!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        response.Value.Should()
            .Be($"Person with id {person.Id} carries weight above the limit 20");
    }

    [Fact]
    public async Task When_CreatePassengerTwiceForTheSameFlight_Then_ShouldReturnBadRequest()
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

        flightRepositoryMock.Setup(p => p.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(flight);
        personRepositoryMock.Setup(p => p.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(person);

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
        await passengersController.Create(dto);
        var response = await passengersController.Create(dto) as ObjectResult;

        // Assert
        response!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        response.Value.Should()
            .Be(
                $"Person with id {person.Id} is already a passenger in flight with id {flight.Id}");
    }

    [Fact]
    public async Task When_CreatePassengerWithBaggageWithWeightAboveTheLimit_Then_ShouldReturnBadRequest()
    {
        var passengerRepositoryMock = new Mock<IRepository<Passenger>>();
        var personRepositoryMock = new Mock<IRepository<Person>>();
        var flightRepositoryMock = new Mock<IRepository<Flight>>();
        var allergyRepositoryMock = new Mock<IRepository<Allergy>>();
        var flight = CreateFlight();
        var person = CreatePersons()[0];
        var baggages = new List<Baggage>
        {
            Baggage.Create(4, 2, 4, 2).Entity!,
            Baggage.Create(11, 2, 2.5, 1.5).Entity!,
            Baggage.Create(5, 1.5, 4.5, 2).Entity!
        };

        var passengersController = new PassengersController(passengerRepositoryMock.Object,
            personRepositoryMock.Object, flightRepositoryMock.Object, allergyRepositoryMock.Object);

        flightRepositoryMock.Setup(p => p.GetAsync(It.IsAny<Guid>())).ReturnsAsync(flight);
        personRepositoryMock.Setup(p => p.GetAsync(It.IsAny<Guid>())).ReturnsAsync(person);

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
        var response = await passengersController.Create(dto) as ObjectResult;

        // Assert
        response!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        response.Value.Should()
            .Be(
                $"Person with id {person.Id} carries a baggage with the weight above the limit of {flight.MaxWeightPerBaggage}");
    }

    [Fact]
    public async Task When_CreatePassengerWithBaggageWithDimensionsAboveTheLimit_Then_ShouldReturnBadRequest()
    {
        var passengerRepositoryMock = new Mock<IRepository<Passenger>>();
        var personRepositoryMock = new Mock<IRepository<Person>>();
        var flightRepositoryMock = new Mock<IRepository<Flight>>();
        var allergyRepositoryMock = new Mock<IRepository<Allergy>>();
        var flight = CreateFlight();
        var person = CreatePersons()[0];
        var baggages = new List<Baggage>
        {
            Baggage.Create(4, 2, 4, 10).Entity !,
            Baggage.Create(10, 2, 2.5, 1.5).Entity !,
            Baggage.Create(5, 1.5, 4.5, 2).Entity !
        };

        var passengersController = new PassengersController(passengerRepositoryMock.Object,
            personRepositoryMock.Object, flightRepositoryMock.Object, allergyRepositoryMock.Object);

        flightRepositoryMock.Setup(p => p.GetAsync(It.IsAny<Guid>())).ReturnsAsync(flight);
        personRepositoryMock.Setup(p => p.GetAsync(It.IsAny<Guid>())).ReturnsAsync(person);

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
        var response = await passengersController.Create(dto) as ObjectResult;

        // Assert
        response!.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        response.Value.Should()
            .Be(
                $"Person with id {person.Id} carries a baggage that has dimensions above the limit of {flight.MaxBaggageWidth} - {flight.MaxBaggageHeight} - {flight.MaxBaggageLength}");
    }

    [Fact]
    public async Task When_GetPassengersForFlights_Then_ShouldReturnPassengers()
    {
        // Arrange
        var passenger = CreatePassengers()[0];
        await Context.Passengers.AddAsync(passenger);
        await Context.SaveChangesAsync();

        // Act
        var responseMessage = await HttpClient.GetAsync(ApiUrl + $"?flightId={passenger.Flight.Id}");
        var response = await responseMessage.Content.ReadFromJsonAsync<List<Passenger>>();

        // Assert
        responseMessage.EnsureSuccessStatusCode();
        response!.Count.Should().Be(2);
    }


    [Fact]
    public async Task When_GetPassenger_Then_ShouldReturnPassenger()
    {
        // Arrange
        var passengers = CreatePassengers();
        await Context.Passengers.AddAsync(passengers[0]);
        await Context.Passengers.AddAsync(passengers[1]);
        await Context.SaveChangesAsync();

        // Act
        var responseMessage = await HttpClient.GetAsync(ApiUrl + $"{passengers[0].Id}");
        var response = await responseMessage.Content.ReadFromJsonAsync<Passenger>();

        // Assert
        responseMessage.EnsureSuccessStatusCode();
        response!.Person.Name.Should().Be("John");
        response.Person.Surname.Should().Be("Doe");
        response.Person.Gender.Should().Be(Gender.Male);
    }

    [Fact]
    public async Task When_DeletePassenger_Then_ShouldDeletePassenger()
    {
        // Arrange
        var passenger = CreatePassengers()[0];
        await Context.Passengers.AddAsync(passenger);
        await Context.SaveChangesAsync();

        // Act
        var responseMessage = await HttpClient.DeleteAsync(ApiUrl + $"{passenger.Id}");

        // Assert
        responseMessage.EnsureSuccessStatusCode();
        responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    private static List<Passenger> CreatePassengers()
    {
        var persons = CreatePersons();
        var flight = CreateFlight();

        var passengers = persons.Select(person =>
            Passenger.Create(person, flight, 80, CreateBaggages(), CreateAllergies()).Entity).ToList();

        return passengers!;
    }

    private static List<Allergy> CreateAllergies()
    {
        return new List<Allergy>()
        {
            Allergy.Create("Dust").Entity!,
            Allergy.Create("Hay").Entity!
        };
    }

    private static List<Baggage> CreateBaggages()
    {
        return new List<Baggage>
        {
            Baggage.Create(10, 2, 1.5, 2).Entity!,
            Baggage.Create(5, 1.5, 4.5, 2).Entity!
        };
    }

    private static Flight CreateFlight()
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
            .Entity!;
    }

    private static Airport CreateDepartureAirport()
    {
        var address = CreateAddress1();
        return Airport.Create("Wizz Airport", address).Entity!;
    }

    private static Airport CreateDestinationAirport()
    {
        var address = CreateAddress2();
        return Airport.Create("Suceava Airport", address).Entity!;
    }

    private static Address CreateAddress1()
    {
        var country = CreateCountry();
        var city = City.Create("Suceava", country).Entity!;
        return Address.Create("100", "Carol 1", city, country).Entity!;
    }

    private static Address CreateAddress2()
    {
        var country = CreateCountry();
        var city = City.Create("Suceava", country).Entity!;
        return Address.Create("2087", "Mihai Eminescu", city, country).Entity!;
    }

    private static Country CreateCountry()
    {
        return Country.Create("Romania", "RO").Entity!;
    }

    private static List<Person> CreatePersons()
    {
        var address = CreateAddress1();

        return new List<Person>
        {
            Person.Create("John", "Doe", new DateTime(1998, 10, 11), "Male", address).Entity!,
            Person.Create("Emma", "Doe", new DateTime(1998, 10, 11), "Female", address).Entity!
        };
    }
}