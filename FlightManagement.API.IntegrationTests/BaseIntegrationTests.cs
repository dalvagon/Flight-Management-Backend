using FlightManagement.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;

namespace FlightManagement.API.IntegrationTests
{
    public class BaseIntegrationTests<T> where T : class
    {
        protected HttpClient HttpClient { get; private set; }

        protected BaseIntegrationTests()
        {
            var application = new WebApplicationFactory<T>().WithWebHostBuilder(_ => { });

            HttpClient = application.CreateClient();
            CleanDatabases();
        }

        private void CleanDatabases()
        {
            var databaseContext = new DatabaseContext();
            databaseContext.Addresses.RemoveRange(databaseContext.Addresses);
            databaseContext.Administrators.RemoveRange(databaseContext.Administrators);
            databaseContext.Airports.RemoveRange(databaseContext.Airports);
            databaseContext.Baggages.RemoveRange(databaseContext.Baggages);
            databaseContext.Companies.RemoveRange(databaseContext.Companies);
            databaseContext.Flights.RemoveRange(databaseContext.Flights);
            databaseContext.Passengers.RemoveRange(databaseContext.Passengers);
            databaseContext.People.RemoveRange(databaseContext.People);

            databaseContext.SaveChanges();
        }
    }
}