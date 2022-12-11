using System.Collections;
using System.Collections.Immutable;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IRepository<Country> _countryRepository;

        public CountriesController(IRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var countries = await _countryRepository.AllAsync();
            return Ok(countries);
        }
    }
}