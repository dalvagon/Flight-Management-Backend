using System.Net.Http.Json;
using FlightManagement.API.Controllers;
using FlightManagement.Domain.Entities;
using FluentAssertions;

namespace FlightManagement.API.IntegrationTests
{
    public class BaggageTests : BaseIntegrationTests<BaggagesController>
    {
        private const string ApiUrl = "/api/v1/baggages/";

        [Fact]
        public async Task When_GetBaggages_Then_ShouldReturnBaggages()
        {
            // Arrange
            var passenger = CreatePassengers()[0];
            await Context.Passengers.AddAsync(passenger);
            await Context.SaveChangesAsync();

            // Act
            var responseMessage = await HttpClient.GetAsync(ApiUrl);
            var response = await responseMessage.Content.ReadFromJsonAsync<List<Baggage>>();

            // Assert
            responseMessage.EnsureSuccessStatusCode();
            response!.Count.Should().Be(4);
        }

        private static List<Passenger> CreatePassengers()
        {
            var persons = CreatePersons();
            var flight = CreateFlight();

            var passengers = persons.Select(person =>
                Passenger.Create(person, flight, 80, CreateBaggages(), new List<Allergy>()).Entity).ToList();

            return passengers!;
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
}