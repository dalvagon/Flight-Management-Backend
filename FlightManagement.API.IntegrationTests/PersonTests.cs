using System.Net;
using System.Net.Http.Json;
using FlightManagement.API.Controllers;
using FlightManagement.API.Dtos;
using FlightManagement.API.Features.Persons;
using FluentAssertions;

namespace FlightManagement.API.IntegrationTests
{
    public class PersonTests : BaseIntegrationTests<PeopleController>
    {
        private const string ApiUrl = "/api/v1/people";

        [Fact]
        public async Task When_CreatePerson_Then_ShouldReturnPersonAsync()
        {
            // Arrange
            CreatePersonDto dto = GetPersonDto();

            // Act
            var response = await HttpClient.PostAsJsonAsync(ApiUrl, dto);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task When_CreatePersonWithInvalidGender_Then_ShouldReturnBadRequest()
        {
            // Arrange
            CreatePersonDto dto = GetPersonDto();
            dto.Gender = "MALE";

            // Act
            var response = await HttpClient.PostAsJsonAsync(ApiUrl, dto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Content.ReadAsStringAsync().Result.Should()
                .Be($"The provided gender {dto.Gender} is not one from the values: Male, Female");
        }

        private CreatePersonDto GetPersonDto()
        {
            return new CreatePersonDto
            {
                Name = "John",
                Surname = "Doe",
                Age = 21,
                Gender = "Male",
                AddressDto = new CreateAddressDto
                {
                    Number = "100",
                    Street = "Oak Street",
                    City = "London",
                    Country = "Uk"
                }
            };
        }
    }
}