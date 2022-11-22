using FlightManagement.Business.Entities;
using FlightManagement.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace FlightManagement.Business.Tests
{
    public class FlightsTest
    {
        [Fact]
        public void WhenAddPassengersToFlight_Then_ShouldReturnSucces()
        {
            // Arrange
            var flight = CreateFlight();
            var passengers = CreatePassengers();

            // Act
            var result = flight.AttachPassengersToFlight(passengers);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void WhenAddDuplicatedPassengersToFlight_Then_ShouldReturnFailure()
        {
            // Arrange
            var flight = CreateFlight();
            var passengers = CreatePassengers();

            // Act
            flight.AttachPassengersToFlight(passengers);
            var result = flight.AttachPassengersToFlight(passengers.Take(2).ToList());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error
                .Should()
                .Be(
                    "Person with id "
                        + passengers[0].Id
                        + " is already a passenger in flight with id "
                        + flight.Id
                );
        }

        private List<Passenger> CreatePassengers()
        {
            var persons = CreatePersons();
            var flight = CreateFlight();
            return new List<Passenger>(
                persons.Select(person => new Passenger(person, flight, 20, null, null)).ToList()
            );
        }

        private Flight CreateFlight()
        {
            var airport = CreateAirport();
            return new Flight(
                new DateTime(2022, 11, 23, 10, 30, 0),
                new DateTime(2022, 11, 23, 22, 30, 0),
                200,
                10000,
                airport
            );
        }

        private Airport CreateAirport()
        {
            var address = CreateAddress();
            return new Airport("Wizz Airport", address, "Bucharest");
        }

        private Address CreateAddress()
        {
            return new Address("100", "Carol 1", "Bucharest", "Romania");
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
