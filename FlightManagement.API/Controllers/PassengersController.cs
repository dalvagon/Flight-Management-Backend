using FlightManagement.API.Dtos;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class PassengersController : ControllerBase
{
    private readonly IRepository<Allergy> _allergyRepository;
    private readonly IRepository<Flight> _flightRepository;
    private readonly IRepository<Passenger> _passengerRepository;
    private readonly IRepository<Person> _personRepository;

    public PassengersController(IRepository<Passenger> passengerRepository, IRepository<Person> personRepository,
        IRepository<Flight> flightRepository, IRepository<Allergy> allergyRepository)
    {
        _passengerRepository = passengerRepository;
        _personRepository = personRepository;
        _flightRepository = flightRepository;
        _allergyRepository = allergyRepository;
    }

    [HttpGet]
    public async Task<IActionResult> AllForFlight([FromQuery] Guid flightId)
    {
        var passengers = await _passengerRepository.AllAsync();

        return Ok(passengers.Where(p => p.Flight.Id == flightId));
    }

    [HttpGet("{passengerId:guid}")]
    public async Task<IActionResult> Get(Guid passengerId)
    {
        return Ok(await _passengerRepository.GetAsync(passengerId));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePassengerDto dto)
    {
        var person = await _personRepository.GetAsync(dto.PersonId);

        var flight = await _flightRepository.GetAsync(dto.FlightId);

        var baggages = dto.BaggageDtos
            .Select(d => Baggage.Create(d.Weight, d.Width, d.Height, d.Length).Entity!)
            .ToList();
        var allergies = new List<Allergy>();

        async void Action(Guid id)
        {
            var allergy = await _allergyRepository.GetAsync(id);
            if (allergy != null)
            {
                allergies.Add(allergy);
            }
        }

        dto.AllergyIds!.ForEach(Action);

        var result = Passenger.Create(person!, flight!, dto.Weight, baggages, allergies);
        if (result.IsFailure) return BadRequest(result.Error);

        var passenger = result.Entity!;
        await _passengerRepository.AddAsync(passenger);
        _passengerRepository.SaveChangesAsync();

        return Created(nameof(Get), passenger);
    }

    [HttpDelete("{passengerId:guid}")]
    public async Task<IActionResult> Delete(Guid passengerId)
    {
        var passenger = await _passengerRepository.GetAsync(passengerId);

        _passengerRepository.Delete(passenger!);
        _passengerRepository.SaveChangesAsync();

        return NoContent();
    }
}