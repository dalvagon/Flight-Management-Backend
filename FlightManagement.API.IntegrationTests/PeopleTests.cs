using FlightManagement.API.Controllers;
using FlightManagement.API.Features.Persons;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;

namespace FlightManagement.API.IntegrationTests
{
    public class PeopleTests : BaseIntegrationTests<PeopleController>
    {
        private const string ApiUrl = "/api/v1/people";

        [Fact]
        public async Task When_CreatePeople_Then_ShouldReturnPeopleAsync()
        {
            // Arrange
            CreatePersonDto dto = new CreatePersonDto
            {
                Name = "John",
                Surname = "Doe",
                Age = 21,
                Gender = "Male"
            };

            // Act
            var createEntity1Response = await HttpClient.PostAsJsonAsync(ApiUrl, dto);

            // Assert
            createEntity1Response.EnsureSuccessStatusCode();
            createEntity1Response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}
