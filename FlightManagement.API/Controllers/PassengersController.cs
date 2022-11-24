using FlightManagement.API.Dtos;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengersController : ControllerBase
    {
        private readonly IRepository<Passenger> _passengerRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<Flight> _flightRepository;

        public PassengersController(IRepository<Passenger> passengerRepository, IRepository<Person> personRepository,
            IRepository<Flight> flightRepository)
        {
            _passengerRepository = passengerRepository;
            _personRepository = personRepository;
            _flightRepository = flightRepository;
        }

        [HttpGet]
        public IActionResult All()
        {
            return Ok(_passengerRepository.All());
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

            var passenger = Passenger.Create(person, flight, dto.Weight).Entity;
            _passengerRepository.Add(passenger);
            _passengerRepository.SaveChanges();

            return Created(nameof(Get), passenger);
        }
    }
}