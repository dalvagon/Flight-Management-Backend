using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaggagesController : ControllerBase
    {
        private readonly IRepository<Baggage> _baggageRepository;
        private readonly IRepository<Passenger> _passengerRepository;

        public BaggagesController(
            IRepository<Baggage> baggageRepository,
            IRepository<Passenger> passengerRepository
        )
        {
            _baggageRepository = baggageRepository;
            _passengerRepository = passengerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return Ok(await _baggageRepository.AllAsync());
        }

        [HttpGet("{baggageId:guid}")]
        public async Task<IActionResult> Get(Guid baggageId)
        {
            return Ok(await _baggageRepository.GetAsync(baggageId));
        }
    }
}