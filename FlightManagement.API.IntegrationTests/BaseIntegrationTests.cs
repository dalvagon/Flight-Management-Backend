using FlightManagement.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace FlightManagement.API.IntegrationTests;

public class BaseIntegrationTests<T> where T : class
{
    protected HttpClient HttpClient { get; }
    protected DatabaseContext Context { get; }

    protected BaseIntegrationTests()
    {
        var server = new TestServer(new WebHostBuilder()
            .UseStartup<Startup<T>>());
        HttpClient = server.CreateClient();
        Context = server.Services.GetService(typeof(DatabaseContext)) as DatabaseContext ??
                  throw new InvalidOperationException();
    }
}