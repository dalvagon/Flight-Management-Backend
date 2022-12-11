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
        public async Task<IActionResult> All()
        {
            return Ok(await _flightRepository.AllAsync());
        }

        [HttpGet("{flightId:guid}")]
        public async Task<IActionResult> All(Guid flightId)
        {
            return Ok(await _flightRepository.GetAsync(flightId));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFlightDto dto)
        {
            var departureAirport = await _airpotRepository.GetAsync(dto.DepartureAirportId);
            var destinationAirport = await _airpotRepository.GetAsync(dto.DestinationAirportId);
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
            await _flightRepository.AddAsync(flight);
            _flightRepository.SaveChangesAsync();

            return Created(nameof(All), flight);
        }

        [HttpPatch("{flightId:guid}/delay")]
        public async Task<IActionResult> Update(Guid flightId, [FromBody] UpdateFlightDto dto)
        {
            var flight = await _flightRepository.GetAsync(flightId);

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

            await _flightRepository.UpdateAsync(newFlight);
            _flightRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{flightId:guid}")]
        public async Task<IActionResult> Remove(Guid flightId)
        {
            var flight = await _flightRepository.GetAsync(flightId);
            _flightRepository.DeleteAsync(flight);
            _flightRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}