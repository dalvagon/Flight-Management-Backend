using FlightManagement.Application.Commands;
using FlightManagement.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class AirportsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AirportsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var result = await _mediator.Send(new GetAllAirportsQuery());

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Entity);
    }

    [HttpGet("byCity")]
    public async Task<IActionResult> AllByCity([FromQuery] string cityName)
    {
        var result = await _mediator.Send(new GetAllAirportsByCityQuery() { CityName = cityName });

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Entity);
    }

    [HttpGet("{airportId:guid}")]
    public async Task<IActionResult> Get(Guid airportId)
    {
        var result = await _mediator.Send(new GetAirportQuery() { AirportId = airportId });
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Entity);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAirportCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Created(nameof(Get), result.Entity);
    }

    [HttpDelete("{airportId:guid}")]
    public async Task<IActionResult> Delete(Guid airportId)
    {
        var result = await _mediator.Send(new DeleteAirportCommand() { AirportId = airportId });
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }
}