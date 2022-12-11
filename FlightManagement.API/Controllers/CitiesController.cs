using FlightManagement.API.Dtos;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Country> _countryRepository;

        public CitiesController(IRepository<City> cityRepository, IRepository<Country> countryRepository)
        {
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> AllFromCountry([FromQuery] string countryName)
        {
            return Ok(await _cityRepository.FindAsync(c => c.Country.Name == countryName));
        }
    }
}