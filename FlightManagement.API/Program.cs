using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure;
using FlightManagement.Infrastructure.Generics;
using FlightManagement.Infrastructure.Generics.GenericRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DatabaseContext>();
builder.Services.AddScoped<IRepository<Address>, AddressRepository>();
builder.Services.AddScoped<IRepository<Administrator>, AdministratorRepository>();
builder.Services.AddScoped<IRepository<Airport>, AirportRepository>();
builder.Services.AddScoped<IRepository<Allergy>, AllergyRepository>();
builder.Services.AddScoped<IRepository<Baggage>, BaggageRepository>();
builder.Services.AddScoped<IRepository<Company>, CompanyRepository>();
builder.Services.AddScoped<IRepository<Flight>, FLightRepository>();
builder.Services.AddScoped<IRepository<Passenger>, PassengerRepository>();
builder.Services.AddScoped<IRepository<Person>, PersonRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();