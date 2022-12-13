using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FluentAssertions;
using Xunit;

namespace FlightManagement.Business.Tests
{
    public class AdministratorTests
    {
        [Fact]
        public void When_AddPassengersToFlight_Then_ShouldReturnSuccess()
        {
            // Arrange
            var result = CreateAdministrator();

            // Act

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        private static Result<Administrator> CreateAdministrator()
        {
            return Administrator.Create(CreateCompany(), CreatePersons()[0]);
        }

        private static Company CreateCompany()
        {
            return Company.Create("Pravel", new DateTime(1999, 6, 12)).Entity!;
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

        private static Address CreateAddress1()
        {
            var country = CreateCountry();
            var city = City.Create("Suceava", country).Entity!;
            return Address.Create("100", "Carol 1", city, country).Entity!;
        }

        private static Country CreateCountry()
        {
            return Country.Create("Romania", "RO").Entity!;
        }
    }
}