using FlightManagement.API.Dtos;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class AirportsController : ControllerBase
{
    private readonly IRepository<City> _cityRepository;
    private readonly IRepository<Country> _countryRepository;
    private readonly IRepository<Airport> _airportRepository;

    public AirportsController(IRepository<Airport> airportRepository,
        IRepository<City> cityRepository, IRepository<Country> countryRepository)
    {
        _airportRepository = airportRepository;
        _cityRepository = cityRepository;
        _countryRepository = countryRepository;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        return Ok(await _airportRepository.AllAsync());
    }

    [HttpGet("byCity")]
    public async Task<IActionResult> AllByCity([FromQuery] string cityName)
    {
        return Ok(await _airportRepository.FindAsync(a => a.Address.City.Name == cityName));
    }

    [HttpGet("{airportId:guid}")]
    public async Task<IActionResult> Get(Guid airportId)
    {
        return Ok(await _airportRepository.GetAsync(airportId));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAirportDto dto)
    {
        var country = await _countryRepository.GetAsync(dto.Address.CountryId);
        var city = await _cityRepository.GetAsync(dto.Address.CityId);
        var address = Address.Create(dto.Address.Number, dto.Address.Street, city!, country!).Entity;

        var airport = Airport.Create(dto.Name, address!).Entity;

        await _airportRepository.AddAsync(airport!);
        _airportRepository.SaveChangesAsync();

        return Created(nameof(Get), airport);
    }

    [HttpDelete("{airportId:guid}")]
    public async Task<IActionResult> Delete(Guid airportId)
    {
        var airport = await _airportRepository.GetAsync(airportId);

        _airportRepository.Delete(airport!);
        _airportRepository.SaveChangesAsync();

        return NoContent();
    }
}