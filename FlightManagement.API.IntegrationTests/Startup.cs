﻿using System.Data.Common;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure;
using FlightManagement.Infrastructure.Generics;
using FlightManagement.Infrastructure.Generics.GenericRepositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace FlightManagement.API.IntegrationTests;

public class Startup<T> : WebApplicationFactory<T> where T : class
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddMvc().AddApplicationPart(typeof(T).Assembly);

        services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        );

        services.AddSingleton<DbConnection>(container =>
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            return connection;
        });
        services.AddDbContext<DatabaseContext>((container, options) =>
        {
            var connection = container.GetRequiredService<DbConnection>();
            options.UseSqlite(connection);
        });

        services.AddScoped<IRepository<Address>, AddressRepository>();
        services.AddScoped<IRepository<Administrator>, AdministratorRepository>();
        services.AddScoped<IRepository<Airport>, AirportRepository>();
        services.AddScoped<IRepository<Allergy>, AllergyRepository>();
        services.AddScoped<IRepository<Baggage>, BaggageRepository>();
        services.AddScoped<IRepository<City>, CityRepository>();
        services.AddScoped<IRepository<Company>, CompanyRepository>();
        services.AddScoped<IRepository<Country>, CountryRepository>();
        services.AddScoped<IRepository<Flight>, FlightRepository>();
        services.AddScoped<IRepository<Passenger>, PassengerRepository>();
        services.AddScoped<IRepository<Person>, PersonRepository>();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
            endpoints.MapControllers());
    }
}