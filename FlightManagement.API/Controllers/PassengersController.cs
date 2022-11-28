using FlightManagement.API.Dtos;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PassengersController : ControllerBase
    {
        private readonly IRepository<Passenger> _passengerRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<Flight> _flightRepository;
        private readonly IRepository<Allergy> _allergyRepository;
        private readonly IRepository<Baggage> _baggageRepository;

        public PassengersController(IRepository<Passenger> passengerRepository, IRepository<Person> personRepository,
            IRepository<Flight> flightRepository, IRepository<Allergy> allergyRepository,
            IRepository<Baggage> baggageRepository)
        {
            _passengerRepository = passengerRepository;
            _personRepository = personRepository;
            _flightRepository = flightRepository;
            _allergyRepository = allergyRepository;
            _baggageRepository = baggageRepository;
        }

        [HttpGet]
        public IActionResult All()
        {
            return Ok(_passengerRepository.All());
        }

        [HttpGet("byFlight")]
        public IActionResult AllForFlight([FromQuery] Guid flightId)
        {
            var passengers = _passengerRepository.All();

            return Ok(passengers.Where(p => p.Flight.Id == flightId));
        }

        [HttpGet("{passengerId:guid}")]
        public IActionResult Get(Guid passengerId)
        {
            return Ok(_passengerRepository.Get(passengerId));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreatePassengerDto dto)
        {
            var person = _personRepository.Get(dto.PersonId);
            var flight = _flightRepository.Get(dto.FlightId);
            var baggages = dto.BaggageDtos.Select(dto => new Baggage(dto.Weight, dto.Width, dto.Height, dto.Length))
                .ToList();
            var allergies = dto.AllergyIds.Select(id => _allergyRepository.Get(id)).ToList();

            var result = Passenger.Create(person, flight, dto.Weight, baggages, allergies);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            var passenger = result.Entity;
            _passengerRepository.Add(passenger);
            _passengerRepository.SaveChanges();

            return Created(nameof(Get), passenger);
        }

        [HttpDelete]
        public IActionResult Delete(Guid passengerId)
        {
            var passenger = _passengerRepository.Get(passengerId);

            _passengerRepository.Delete(passenger);
            _passengerRepository.SaveChanges();

            return NoContent();
        }
    }
}