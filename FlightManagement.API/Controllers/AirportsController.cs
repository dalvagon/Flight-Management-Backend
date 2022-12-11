using FlightManagement.API.Dtos;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AirportsController : ControllerBase
    {
        private readonly IRepository<Airport> _airportRepository;
        private readonly IRepository<Address> _addressRepository;

        public AirportsController(IRepository<Airport> airportRepository, IRepository<Address> addressRepository)
        {
            _airportRepository = airportRepository;
            _addressRepository = addressRepository;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return Ok(await (_airportRepository).AllAsync());
        }

        [HttpGet("{airportId:guid}")]
        public async Task<IActionResult> Get(Guid airportId)
        {
            return Ok(await _airportRepository.GetAsync(airportId));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAirportDto dto)
        {
            var address = await _addressRepository.GetAsync(dto.AddressId);

            var airport = Airport.Create(dto.Name, address).Entity;

            await _airportRepository.AddAsync(airport);
            _airportRepository.SaveChangesAsync();

            return Created(nameof(Get), airport);
        }

        [HttpDelete("{airportId:guid}")]
        public async Task<IActionResult> Delete(Guid airportId)
        {
            var airport = await _airportRepository.GetAsync(airportId);

            _airportRepository.DeleteAsync(airport);
            _airportRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}