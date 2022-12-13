using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FluentAssertions;
using Xunit;

namespace FlightManagement.Business.Tests;

public class FlightsTest
{
    [Fact]
    public void When_AddPassengersToFlight_Then_ShouldReturnSuccess()
    {
        // Arrange
        var flight = CreateFlight();
        var passengers = CreatePassengers();

        // Act
        var result = flight.AttachPassengerToFlight(passengers[0]);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void When_AddDuplicatedPassengersToFlight_Then_ShouldReturnFailure()
    {
        // Arrange
        var flight = CreateFlight();
        var passengers = CreatePassengers();

        // Act
        passengers.ForEach(passenger => flight.AttachPassengerToFlight(passenger));
        var result = flight.AttachPassengerToFlight(passengers[0]);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error
            .Should()
            .Be(
                $"Person with id {passengers[0].Person.Id} is already a passenger in flight with id {flight.Id}"
            );
    }

    [Fact]
    public void When_CreateFlightWithTheDepartureDatePastTheArrivalDate_Then_ShouldReturnFailure()
    {
        // Arrange
        var result = CreateBadFlight1();

        // Act

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error
            .Should()
            .Be(
                $"The departure date {new DateTime(2022, 11, 23, 11, 30, 0)} " +
                $"for the flight is past the arrival date {new DateTime(2022, 11, 23, 10, 30, 0)}");
    }

    [Fact]
    public void When_AddPassengersToFlightWithNoSeatsLeft_Then_ShouldReturnFailure()
    {
        // Arrange
        var flight = CreateBadFlight2();

        // Act
        var passengers = CreatePassengers();
        var result = flight.AttachPassengerToFlight(passengers[0]);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error
            .Should()
            .Be(
                $"There aren't any seats left on flight with id {flight.Id}");
    }

    [Fact]
    public void When_AddPassengersWithBaggageDimensionsAboveLimit_Then_ShouldReturnFailure()
    {
        // Arrange
        var flight = CreateFlight();
        var passengers = CreatePassengers();
        var passenger = passengers[0];

        passenger.AttachBaggages(
            new List<Baggage>
            {
                Baggage.Create(2, 1.5, 5, 2.4).Entity!,
                Baggage.Create(5, 2.5, 2, 1.6).Entity!,
                Baggage.Create(10, 2, 1.5, 2).Entity!
            }
        );

        // Act
        var result = flight.AttachPassengerToFlight(passengers[0]);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error
            .Should()
            .Be(
                $"Person with id {passenger.Person.Id} carries a baggage that has dimensions above the limit of {flight.MaxBaggageWidth} - {flight.MaxBaggageHeight} - {flight.MaxBaggageLength}"
            );
    }

    [Fact]
    public void When_AddPassengersWithBaggageWeightAboveMaxBaggageWeightPerPersonLimit_Then_ShouldReturnFailure()
    {
        // Arrange
        var flight = CreateFlight();
        var passengers = CreatePassengers();
        var passenger = passengers[0];

        passenger.AttachBaggages(
            new List<Baggage>
            {
                Baggage.Create(15, 1.5, 2, 2.4).Entity!,
                Baggage.Create(6, 2.5, 2, 1.6).Entity!,
                Baggage.Create(20, 2, 1.5, 2).Entity!
            }
        );

        // Act
        var result = flight.AttachPassengerToFlight(passengers[0]);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error
            .Should()
            .Be(
                $"Person with id {passenger.Person.Id} carries weight above the limit {flight.MaxBaggageWeightPerPassenger}"
            );
    }

    [Fact]
    public void When_AddPassengersWithBaggageWeightAboveBaggageWeightLimit_Then_ShouldReturnFailure()
    {
        // Arrange
        var flight = CreateFlight();
        var passengers = CreatePassengers();
        var passenger = passengers[0];

        passenger.AttachBaggages(
            new List<Baggage>
            {
                Baggage.Create(2, 1.5, 2, 2.4).Entity!,
                Baggage.Create(5, 2, 2, 1.6).Entity!,
                Baggage.Create(11, 2, 1.5, 2).Entity!
            }
        );

        // Act
        var result = flight.AttachPassengerToFlight(passengers[0]);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error
            .Should()
            .Be(
                $"Person with id {passenger.Person.Id} carries a baggage with the weight above the limit of {flight.MaxWeightPerBaggage}"
            );
    }

    private static List<Passenger> CreatePassengers()
    {
        var persons = CreatePersons();
        var flight = CreateFlight();
        var baggages = CreateBaggages();
        var passengers = persons
            .Select(person => Passenger.Create(person, flight, 70, baggages, new List<Allergy>()).Entity!)
            .ToList();

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
                10,
                40,
                2,
                5,
                2.5,
                CreateDepartureAirport(),
                CreateDestinationAirport()
            )
            .Entity!;
    }

    private static Flight CreateBadFlight2()
    {
        return Flight
            .Create(
                new DateTime(2022, 11, 23, 10, 30, 0),
                new DateTime(2022, 11, 23, 22, 30, 0),
                0,
                1000000,
                10,
                40,
                2,
                5,
                2.5,
                CreateDepartureAirport(),
                CreateDestinationAirport()
            )
            .Entity!;
    }

    private static Result<Flight> CreateBadFlight1()
    {
        return Flight
            .Create(
                new DateTime(2022, 11, 23, 11, 30, 0),
                new DateTime(2022, 11, 23, 10, 30, 0),
                200,
                1000000,
                10,
                40,
                2,
                5,
                2.5,
                CreateDepartureAirport(),
                CreateDestinationAirport()
            );
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
        var city = City.Create("Bucharest", country).Entity!;
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