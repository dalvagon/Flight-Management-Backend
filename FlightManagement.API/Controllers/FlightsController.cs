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
            this._flightRepository = flightRepository;
            this._airpotRepository = airpotRepository;
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
            var departureAirpost = _airpotRepository.Get(dto.DepartureAirportId);
            var destinationAirport = _airpotRepository.Get(dto.DestinationAirportId);

            var flight = Flight
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
                    departureAirpost,
                    destinationAirport
                )
                .Entity;

            var result = _flightRepository.Add(flight);
            _flightRepository.SaveChanges();

            return Created(nameof(All), result);
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