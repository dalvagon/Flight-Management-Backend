using System.Net;
using System.Net.Http.Json;
using FlightManagement.API.Controllers;
using FlightManagement.Application.Commands;
using FlightManagement.Application.Mappers;
using FlightManagement.Domain.Entities;
using FluentAssertions;

namespace FlightManagement.API.IntegrationTests;

public class PassengerTests : BaseIntegrationTests<PassengersController>
{
    private const string ApiUrl = "/api/v1/passengers/";

    [Fact]
    public async Task WhenGetPassengersForFlight_Then_ShouldReturnPassengers()
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
    public async Task WhenGetPassenger_Then_ShouldReturnPassenger()
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
    public async Task WhenDeletePassenger_Then_ShouldDeletePassenger()
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

    [Fact]
    public async Task WhenCreatePassenger_Then_ShouldReturnPassenger()
    {
        // Arrange
        var passenger = CreatePassengers()[0];
        var flight = CreateFlight();
        await Context.Countries.AddAsync(passenger.Person.Address.Country);
        await Context.Cities.AddAsync(passenger.Person.Address.City);
        await Context.Addresses.AddAsync(passenger.Person.Address);
        await Context.Flights.AddAsync(flight);
        await Context.Allergies.AddRangeAsync(passenger.Allergies);
        await Context.People.AddAsync(passenger.Person);
        await Context.SaveChangesAsync();
        var command = new CreatePassengerCommand()
        {
            PersonId = passenger.Person.Id,
            FlightId = flight.Id,
            Weight = passenger.Weight,
            Baggages = passenger.Baggages.Select(b => BaggageMapper.Mapper.Map<CreateBaggageCommand>(b)).ToList(),
            AllergyIds = passenger.Allergies.Select(a => a.Id).ToList()
        };

        // Act
        var result = await HttpClient.PostAsJsonAsync(ApiUrl, command);
        var passengerResult = await result.Content.ReadFromJsonAsync<Passenger>();

        // Assert
        result.EnsureSuccessStatusCode();
        result.StatusCode.Should().Be(HttpStatusCode.Created);
        passengerResult!.Person.Name.Should().Be(passenger.Person.Name);
        passengerResult.Allergies.Count.Should().Be(2);
        passengerResult.Baggages.Count.Should().Be(2);
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