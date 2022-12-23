using System.Net;
using System.Net.Http.Json;
using FlightManagement.API.Controllers;
using FlightManagement.Application.Commands;
using FlightManagement.Domain.Entities;
using FluentAssertions;

namespace FlightManagement.API.IntegrationTests;

public class FlightTests : BaseIntegrationTests<FlightsController>
{
    private const string ApiUrl = "/api/v1/flights/";

    [Fact]
    public async Task WhenCreateFlightWithTheArrivalDatePastTheDepartureDate_Then_ShouldReturnBadRequestAsync()
    {
        // Arrange
        var flight = CreateFlight();
        var departureAirport = CreateDepartureAirport();
        var destinationAirport = CreateDestinationAirport();
        await Context.Airports.AddAsync(departureAirport);
        await Context.Airports.AddAsync(destinationAirport);
        await Context.SaveChangesAsync();

        var command = new CreateFlightCommand()
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
            DepartureAirportId = departureAirport.Id,
            DestinationAirportId = destinationAirport.Id
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiUrl, command);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        response.Content.ReadAsStringAsync().Result.Should().Be(
            $"The departure date {command.DepartureDate} for the flight is past the arrival date {command.ArrivalDate}");
    }


    [Fact]
    public async Task
        WhenCreateFlightWithTheMaxBaggageWeightPerPersonGreaterThanTheBaggageWeightCapacityDividedByThePassengerCapacity_Then_ShouldReturnBadRequestAsync()
    {
        // Arrange
        var flight = CreateFlight();
        var departureAirport = CreateDepartureAirport();
        var destinationAirport = CreateDestinationAirport();
        await Context.Airports.AddAsync(departureAirport);
        await Context.Airports.AddAsync(destinationAirport);
        await Context.SaveChangesAsync();

        var command = new CreateFlightCommand
        {
            DepartureDate = flight.DepartureDate,
            ArrivalDate = flight.ArrivalDate,
            PassengerCapacity = 100,
            BaggageWeightCapacity = 10000,
            MaxWeightPerBaggage = flight.MaxWeightPerBaggage,
            MaxBaggageWeightPerPassenger = 101,
            MaxBaggageWidth = flight.MaxBaggageWidth,
            MaxBaggageHeight = flight.MaxBaggageHeight,
            MaxBaggageLength = flight.MaxBaggageLength,
            DepartureAirportId = departureAirport.Id,
            DestinationAirportId = destinationAirport.Id
        };

        // Act
        var response = await HttpClient.PostAsJsonAsync(ApiUrl, command);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        response.Content.ReadAsStringAsync().Result.Should().Be(
            $"The maximum baggage weight per passenger ({101}) shouldn't be greater than the baggage capacity ({10000}) divided by the maximum number of passengers ({100})");
    }

    [Fact]
    public async Task WhenGetFlight_Then_ShouldReturnFlight()
    {
        // Arrange
        var flight = CreateFlight();
        var passengers = CreatePassengersForFlight(flight);
        await Context.Flights.AddAsync(passengers[0].Flight);
        await Context.SaveChangesAsync();

        // Act
        var responseMessage = await HttpClient.GetAsync(ApiUrl + flight.Id);
        var response = await responseMessage.Content.ReadFromJsonAsync<Flight>();

        // Assert
        responseMessage.EnsureSuccessStatusCode();
        responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        response!.Passengers.Count.Should().Be(3);
    }

    [Fact]
    public async Task WhenGetFlights_Then_ShouldReturnFlights()
    {
        // Arrange
        var flight = CreateFlight();
        var passengers = CreatePassengersForFlight(flight);
        await Context.Flights.AddAsync(passengers[0].Flight);
        await Context.SaveChangesAsync();

        // Act
        var responseMessage = await HttpClient.GetAsync(ApiUrl + "all");
        var response = await responseMessage.Content.ReadFromJsonAsync<List<Flight>>();

        // Assert
        responseMessage.EnsureSuccessStatusCode();
        responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        response!.Count.Should().Be(1);
    }

    [Fact]
    public async Task WhenGetFlightByDepartureAndDestinationCities_Then_ShouldReturnFlights()
    {
        // Arrange
        var flight = CreateFlight();
        var passengers = CreatePassengersForFlight(flight);
        await Context.Flights.AddAsync(passengers[0].Flight);
        await Context.SaveChangesAsync();

        // Act
        var responseMessage = await HttpClient.GetAsync(ApiUrl +
                                                        $"?departureCity={flight.DepartureAirport.Address.City.Name}&destinationCity={flight.DestinationAirport.Address.City.Name}");
        var response = await responseMessage.Content.ReadFromJsonAsync<List<Flight>>();

        // Assert
        responseMessage.EnsureSuccessStatusCode();
        responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        response!.Count.Should().Be(1);
    }

    [Fact]
    public async Task WhenCreateFlight_Then_ShouldReturnFlight()
    {
        // Arrange
        var flight = CreateFlight();
        await Context.Countries.AddAsync(flight.DepartureAirport.Address.Country);
        await Context.Cities.AddAsync(flight.DestinationAirport.Address.City);
        await Context.Countries.AddAsync(flight.DestinationAirport.Address.Country);
        await Context.Addresses.AddAsync(flight.DestinationAirport.Address);
        await Context.Addresses.AddAsync(flight.DestinationAirport.Address);
        await Context.Cities.AddAsync(flight.DepartureAirport.Address.City);
        await Context.Airports.AddAsync(flight.DepartureAirport);
        await Context.Airports.AddAsync(flight.DestinationAirport);
        await Context.SaveChangesAsync();
        var command = new CreateFlightCommand()
        {
            DepartureDate = flight.DepartureDate,
            ArrivalDate = flight.ArrivalDate,
            PassengerCapacity = flight.PassengerCapacity,
            BaggageWeightCapacity = flight.BaggageWeightCapacity,
            MaxWeightPerBaggage = flight.MaxWeightPerBaggage,
            MaxBaggageWeightPerPassenger = flight.MaxBaggageWeightPerPassenger,
            MaxBaggageHeight = flight.MaxBaggageHeight,
            MaxBaggageLength = flight.MaxBaggageLength,
            MaxBaggageWidth = flight.MaxBaggageWidth,
            DestinationAirportId = flight.DestinationAirport.Id,
            DepartureAirportId = flight.DepartureAirport.Id
        };

        // Act
        var responseMessage = await HttpClient.PostAsJsonAsync(ApiUrl, command);
        var response = await responseMessage.Content.ReadFromJsonAsync<Flight>();

        // Assert
        responseMessage.EnsureSuccessStatusCode();
        responseMessage.StatusCode.Should().Be(HttpStatusCode.Created);
        response!.Passengers.Count.Should().Be(0);
        response.DestinationAirport.Address.Country.Name.Should().Be(flight.DestinationAirport.Address.Country.Name);
        response.DestinationAirport.Address.City.Name.Should().Be(flight.DestinationAirport.Address.City.Name);
        response.DepartureDate.Should().Be(flight.DepartureDate);
        response.ArrivalDate.Should().Be(flight.ArrivalDate);
    }

    private static List<Passenger> CreatePassengersForFlight(Flight flight)
    {
        var persons = CreatePersons();
        var baggages = CreateBaggages();

        var passengers = persons.Select(person =>
            Passenger.Create(person, flight, 80, baggages, new List<Allergy>()).Entity!).ToList();

        return passengers;
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
                20,
                40,
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
            Person.Create("Al", "Pacino", new DateTime(2000, 1, 24), "Male", address).Entity!,
            Person.Create("Ina", "Jackson", new DateTime(1979, 5, 1), "Female", address).Entity!
        };
    }
}