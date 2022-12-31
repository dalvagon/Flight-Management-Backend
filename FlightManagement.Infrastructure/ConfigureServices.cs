using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using FlightManagement.Infrastructure.Generics.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlightManagement.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IRepository<Address>, AddressRepository>();
            services.AddScoped<IRepository<Airport>, AirportRepository>();
            services.AddScoped<IRepository<Allergy>, AllergyRepository>();
            services.AddScoped<IRepository<Baggage>, BaggageRepository>();
            services.AddScoped<IRepository<City>, CityRepository>();
            services.AddScoped<IRepository<Country>, CountryRepository>();
            services.AddScoped<IRepository<Flight>, FlightRepository>();
            services.AddScoped<IRepository<Passenger>, PassengerRepository>();
            services.AddScoped<IRepository<Person>, PersonRepository>();

            services.AddDbContext<DatabaseContext>(m =>
                m.UseSqlite(configuration.GetConnectionString("FlightManagement.db"),
                    b => b.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)));

            return services;
        }
    }
}