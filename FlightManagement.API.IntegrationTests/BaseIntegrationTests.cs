using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace FlightManagement.API.IntegrationTests
{
    public class BaseIntegrationTests<T> where T : class
    {
        protected HttpClient HttpClient { get; }
        private readonly TestServer _server;

        protected BaseIntegrationTests()
        {
            //var application = new WebApplicationFactory<T>().WithWebHostBuilder(_ => { });
            //HttpClient = application.CreateClient();

            //var connectionStringBuilder = new SqliteConnectionStringBuilder
            //    { DataSource = ":memory:" };
            //var connectionString = connectionStringBuilder.ToString();
            //_connection = new SqliteConnection(connectionString);
            //_connection.Open();
            //var options = new DbContextOptionsBuilder<DatabaseContext>()
            //    .UseSqlite(_connection)
            //    .Options;
            //Context = new DatabaseContext(options);


            //var serviceProvider = new ServiceCollection()
            //    .AddEntityFrameworkSqlite()
            //    .BuildServiceProvider();

            //var builder = new DbContextOptionsBuilder<DatabaseContext>();

            //builder.UseSqlite("Data Source = :memory:")
            //    .UseInternalServiceProvider(serviceProvider);

            //_context = new DatabaseContext(builder.Options);

            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup<T>>());
            HttpClient = _server.CreateClient();
        }
    }
}