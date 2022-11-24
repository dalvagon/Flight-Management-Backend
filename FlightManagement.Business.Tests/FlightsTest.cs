using FlightManagement.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace FlightManagement.Business.Tests
{
    public class FlightsTest
    {
        [Fact]
        public void When_AddPassengersToFlight_Then_ShouldReturnSucces()
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
        public void When_AddDuplicatedPassengersToFlight_Then_ShouldReturnFailure()
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

        [Fact]
        public void When_AddPassengersWithBaggageDimensionsAboveLimit_Then_ShouldReturnFailure()
        {
            // Arrange
            var flight = CreateFlight();
            var passengers = CreatePassengers();
            var passenger = passengers[0];

            passenger.AttachBaggages(
                new List<Baggage>()
                {
                    new Baggage(15, 1.5, 5, 2.4),
                    new Baggage(5, 2.5, 2, 1.6),
                    new Baggage(10, 2, 1.5, 2)
                }
            );

            // Act
            var result = flight.AttachPassengersToFlight(passengers);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error
                .Should()
                .Be(
                    "The baggage with id "
                        + passenger.Baggages[0].Id
                        + " of passeger with id "
                        + passenger.Id
                        + " has dimensions above the limit of "
                        + flight.MaxBaggageWidth
                        + " - "
                        + flight.MaxBaggageHeight
                        + " - "
                        + flight.MaxBaggageLength
                );
        }

        [Fact]
        public void When_AddPassengersWithBaggageWeightAboveMaxBaggaeWeightPerPersonLimit_Then_SHouldReturnFailure()
        {
            // Arrange
            var flight = CreateFlight();
            var passengers = CreatePassengers();
            var passenger = passengers[0];

            passenger.AttachBaggages(
                new List<Baggage>()
                {
                    new Baggage(15, 1.5, 2, 2.4),
                    new Baggage(6, 2.5, 2, 1.6),
                    new Baggage(20, 2, 1.5, 2)
                }
            );

            // Act
            var result = flight.AttachPassengersToFlight(passengers);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error
                .Should()
                .Be(
                    "Person with id "
                        + passenger.Id
                        + " carries weight above the limit "
                        + flight.MaxBaggageWeightPerPassenger
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
                new List<Baggage>()
                {
                    new Baggage(21, 1.5, 2, 2.4),
                    new Baggage(2, 2.5, 2, 1.6),
                    new Baggage(17, 2, 1.5, 2)
                }
            );

            // Act
            var result = flight.AttachPassengersToFlight(passengers);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error
                .Should()
                .Be(
                    "The baggage with id "
                        + passenger.Baggages[0].Id
                        + " of passeger with id "
                        + passenger.Id
                        + " has weight above the limit of "
                        + flight.MaxWeightPerBaggage
                );
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
