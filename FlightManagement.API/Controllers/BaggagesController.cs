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
        public IActionResult All()
        {
            return Ok(_baggageRepository.All());
        }

        [HttpGet("{baggageId:guid}")]
        public IActionResult Get(Guid baggageId)
        {
            return Ok(_baggageRepository.Get(baggageId));
        }
    }
}