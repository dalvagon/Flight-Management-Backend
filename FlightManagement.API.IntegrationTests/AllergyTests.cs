using System.Net.Http.Json;
using FlightManagement.API.Controllers;
using FlightManagement.Domain.Entities;
using FluentAssertions;

namespace FlightManagement.API.IntegrationTests
{
    public class AllergyTests : BaseIntegrationTests<AllergiesController>
    {
        private const string ApiUrl = "/api/v1/allergies/";

        [Fact]
        public async Task WhenGetAllergies_Then_ShouldReturnAllergies()
        {
            // Arrange
            var allergy = Allergy.Create("Insect Venom Allergy").Entity;
            await Context.AddAsync(allergy);
            await Context.SaveChangesAsync();

            // Act
            var response = await HttpClient.GetAsync(ApiUrl);
            var allergies = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<Allergy>>();

            // Assert
            response.EnsureSuccessStatusCode();
            allergies.Count.Should().Be(1);
            allergies.First().Should().NotBeNull();
            allergies.First().Name.Should().Be("Insect Venom Allergy");
        }
    }
}