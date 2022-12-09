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
        public IActionResult All()
        {
            return Ok(_airportRepository.All());
        }

        [HttpGet("{airportId:guid}")]
        public IActionResult Get(Guid airportId)
        {
            return Ok(_airportRepository.Get(airportId));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateAirportDto dto)
        {
            var address = _addressRepository.Get(dto.AddressId);

            var airport = Airport.Create(dto.Name, address, dto.City).Entity;

            _airportRepository.Add(airport);
            _airportRepository.SaveChanges();

            return Created(nameof(Get), airport);
        }

        [HttpDelete("{airportId:guid}")]
        public IActionResult Delete(Guid airportId)
        {
            var airport = _airportRepository.Get(airportId);

            _airportRepository.Delete(airport);
            _airportRepository.SaveChanges();

            return NoContent();
        }
    }
}