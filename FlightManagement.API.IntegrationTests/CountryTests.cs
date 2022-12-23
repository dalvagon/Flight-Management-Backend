using System.Net;
using System.Net.Http.Json;
using FlightManagement.API.Controllers;
using FlightManagement.Domain.Entities;
using FluentAssertions;

namespace FlightManagement.API.IntegrationTests
{
    public class CountryTests : BaseIntegrationTests<CountriesController>
    {
        private const string ApiUrl = "/api/v1/countries/";

        [Fact]
        public async Task WhenGetCountriesBeforeSeeding_Then_ShouldReturnBadRequest()
        {
            // Arrange

            // Act
            var response = await HttpClient.GetAsync(ApiUrl);
            var responseMessage = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseMessage.Should().Be("Couldn't get countries");
        }


        [Fact]
        public async Task WhenGetCountriesAfterSeeding_Then_ShouldReturnCountries()
        {
            // Arrange
            var country = Country.Create("Romania", "RO").Entity;
            await Context.Countries.AddAsync(country!);
            await Context.SaveChangesAsync();

            // Act
            var response = await HttpClient.GetAsync(ApiUrl);
            var countries = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<Country>>();

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            countries!.Count.Should().Be(1);
        }
    }
}