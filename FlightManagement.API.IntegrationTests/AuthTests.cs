using System.Net.Http.Json;
using FlightManagement.API.Controllers;
using FlightManagement.Application.Commands;
using FlightManagement.Domain.Entities;
using System.Security.Cryptography;
using FluentAssertions;

namespace FlightManagement.API.IntegrationTests
{
    public class AuthTests : BaseIntegrationTests<AuthController>
    {
        private const string ApiUrl = "/api/v1/auth/";

        [Fact]
        public async Task WhenLogin_Then_ShouldReturnToken()
        {
            // Arrange
            CreatePasswordHash("johndoe1234", out var passwordHash, out var passwordSalt);
            var person = Person.Create("John", "Doe", "john.doe@gmail.com", passwordHash, passwordSalt,
                new DateTime(1985, 11, 9), "Male", CreateAddress()).Entity!;
            await Context.AddAsync(person);
            await Context.SaveChangesAsync();

            var command = new LoginUserCommand()
            {
                Email = "john.doe@gmail.com",
                Password = "johndoe1234"
            };

            // Act
            var response = await HttpClient.PostAsJsonAsync(ApiUrl + "login", command);
            var responseMessage = await response.Content.ReadAsStringAsync();

            // Assert
            responseMessage.Should().NotBe(null);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private static Address CreateAddress()
        {
            var country = CreateCountry();
            var city = CreateCity();

            return Address.Create("1", "Oak Street", city, country).Entity!;
        }

        private static Country CreateCountry()
        {
            return Country.Create("USA", "US").Entity!;
        }

        private static City CreateCity()
        {
            var country = CreateCountry();
            return City.Create("New York", country).Entity!;
        }
    }
}