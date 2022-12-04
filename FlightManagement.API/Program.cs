using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure;
using FlightManagement.Infrastructure.Generics;
using FlightManagement.Infrastructure.Generics.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DatabaseContext>(
    options => options.UseSqlite(
        builder.Configuration.GetConnectionString("FlightManagement.db"),
        b => b.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)));

builder.Services.AddScoped<IRepository<Address>, AddressRepository>();
builder.Services.AddScoped<IRepository<Administrator>, AdministratorRepository>();
builder.Services.AddScoped<IRepository<Airport>, AirportRepository>();
builder.Services.AddScoped<IRepository<Allergy>, AllergyRepository>();
builder.Services.AddScoped<IRepository<Baggage>, BaggageRepository>();
builder.Services.AddScoped<IRepository<Company>, CompanyRepository>();
builder.Services.AddScoped<IRepository<Flight>, FlightRepository>();
builder.Services.AddScoped<IRepository<Passenger>, PassengerRepository>();
builder.Services.AddScoped<IRepository<Person>, PersonRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FlightManagementCors", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("FlightManagementCors");

app.UseAuthorization();

app.MapControllers();

app.Run();