using FlightManagement.API.Dtos;
using FlightManagement.Application.Validator;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class FlightsController : ControllerBase
{
    private readonly IRepository<Airport> _airportRepository;
    private readonly IRepository<Flight> _flightRepository;

    public FlightsController(
        IRepository<Flight> flightRepository,
        IRepository<Airport> airportRepository
    )
    {
        _flightRepository = flightRepository;
        _airportRepository = airportRepository;
    }

    [HttpGet]
    public async Task<IActionResult> AllFromDepartureAndDestinationCities([FromQuery] string departureCity,
        [FromQuery] string destinationCity)
    {
        return Ok(await _flightRepository.FindAsync(flight =>
            flight.DepartureAirport.Address.City.Name == departureCity &&
            flight.DestinationAirport.Address.City.Name == destinationCity));
    }

    [HttpGet("{flightId:guid}")]
    public async Task<IActionResult> All(Guid flightId)
    {
        return Ok(await _flightRepository.GetAsync(flightId));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFlightDto dto)
    {
        var departureAirport = await _airportRepository.GetAsync(dto.DepartureAirportId);
        var destinationAirport = await _airportRepository.GetAsync(dto.DestinationAirportId);

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

        if (result.IsFailure) return BadRequest(result.Error);

        var flight = result.Entity!;
        var validator = new FlightValidator();
        var validatorResult = validator.Validate(flight);

        if (!validatorResult.IsValid)
        {
            return BadRequest("Couldn't create flight");
        }

        await _flightRepository.AddAsync(flight);
        _flightRepository.SaveChangesAsync();

        return Created(nameof(All), flight);
    }
}