using System.Collections.Immutable;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class CitiesController : ControllerBase
{
    private readonly IRepository<City> _cityRepository;

    public CitiesController(IRepository<City> cityRepository)
    {
        _cityRepository = cityRepository;
    }

    [HttpGet]
    public async Task<IActionResult> AllFromCountry([FromQuery] string countryName)
    {
        var cities = await _cityRepository.FindAsync(c => c.Country.Name == countryName);
        cities = cities.OrderBy(c => c.Name).ToImmutableList();

        return Ok(cities);
    }
}