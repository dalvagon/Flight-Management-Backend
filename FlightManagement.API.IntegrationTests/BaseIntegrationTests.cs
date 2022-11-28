using FlightManagement.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.API.IntegrationTests
{
    public class BaseIntegrationTests<T> where T : class
    {
        protected HttpClient HttpClient { get; }

        protected BaseIntegrationTests()
        {
            var application = new WebApplicationFactory<T>().WithWebHostBuilder(_ => { });

            HttpClient = application.CreateClient();
            CleanDatabases();
        }

        private void CleanDatabases()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlite("Data Source = FlightManagementTests.db").Options;

            var databaseContext = new DatabaseContext(options);

            databaseContext.Database.EnsureDeleted();
        }
    }
}