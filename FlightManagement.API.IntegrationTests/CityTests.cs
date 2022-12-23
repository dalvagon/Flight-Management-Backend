using FlightManagement.API.Controllers;
using FlightManagement.Domain.Entities;
using FluentAssertions;
using System.Net.Http.Json;
using System.Net;

namespace FlightManagement.API.IntegrationTests
{
    public class CityTests : BaseIntegrationTests<CitiesController>
    {
        private const string ApiUrl = "/api/v1/cities/";

        [Fact]
        public async Task WhenGetCitiesBeforeSeeding_Then_ShouldReturnBadRequest()
        {
            // Arrange

            // Act
            var response = await HttpClient.GetAsync(ApiUrl + "?countryName=Romania");
            var responseMessage = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseMessage.Should().Be("Couldn't get cities");
        }


        [Fact]
        public async Task WhenGetCountriesAfterSeeding_Then_ShouldReturnCountries()
        {
            // Arrange
            var country = Country.Create("Romania", "RO").Entity;
            var city = City.Create("Iasi", country!).Entity;
            await Context.Countries.AddAsync(country!);
            await Context.Cities.AddAsync(city!);
            await Context.SaveChangesAsync();

            // Act
            var response = await HttpClient.GetAsync(ApiUrl + "?countryName=Romania");
            var countries = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<City>>();

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            countries!.Count.Should().Be(1);
        }
    }
}