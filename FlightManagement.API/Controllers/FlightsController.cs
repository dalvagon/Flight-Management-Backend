using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IRepository<Flight> flightRepository;

        public FlightsController(IRepository<Flight> flightRepository)
        {
            this.flightRepository = flightRepository;
        }

        [HttpGet]
        public IActionResult All()
        {
            return Ok(flightRepository.All());
        }
    }
}
