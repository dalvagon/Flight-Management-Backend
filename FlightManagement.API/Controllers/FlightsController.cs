using FlightManagement.API.Dtos;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IRepository<Flight> _flightRepository;
        private readonly IRepository<Airport> _airpotRepository;

        public FlightsController(
            IRepository<Flight> flightRepository,
            IRepository<Airport> airpotRepository
        )
        {
            _flightRepository = flightRepository;
            _airpotRepository = airpotRepository;
        }

        [HttpGet]
        public IActionResult All()
        {
            return Ok(_flightRepository.All());
        }

        [HttpGet("{flightId:guid}")]
        public IActionResult All(Guid flightId)
        {
            return Ok(_flightRepository.Get(flightId));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateFlightDto dto)
        {
            var departureAirport = _airpotRepository.Get(dto.DepartureAirportId);
            var destinationAirport = _airpotRepository.Get(dto.DestinationAirportId);
            var result = Flight
                .Create(
                    dto.DepartureDate,
                    dto.ArrivalDate,
                    dto.PassengerCapacity,
                    dto.BaggageWeightCapacity,
                    dto.MaxWeightPerBaggage,
                    dto.MaxBaggageWeightPerPassenger,
                    dto.MaxBaggageWidth,
                    dto.MaxBaggageHeight,
                    dto.MaxBaggageLength,
                    departureAirport,
                    destinationAirport
                );

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            var flight = result.Entity;
            _flightRepository.Add(flight);
            _flightRepository.SaveChanges();

            return Created(nameof(All), flight);
        }

        [HttpPatch("{flightId:guid}/delay")]
        public IActionResult Update(Guid flightId, [FromBody] UpdateFlightDto dto)
        {
            var flight = _flightRepository.Get(flightId);

            var newFlight = Flight
                .Create(
                    dto.DepartureDate,
                    dto.ArrivalDate,
                    flight.PassengerCapacity,
                    flight.BaggageWeightCapacity,
                    flight.MaxWeightPerBaggage,
                    flight.MaxBaggageWeightPerPassenger,
                    flight.MaxBaggageWidth,
                    flight.MaxBaggageHeight,
                    flight.MaxBaggageLength,
                    flight.DepartureAirport,
                    flight.DestinationAirport
                )
                .Entity;

            _flightRepository.Update(newFlight);
            _flightRepository.SaveChanges();

            return NoContent();
        }
    }
}