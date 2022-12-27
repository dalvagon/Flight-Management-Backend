using FlightManagement.Application.Commands;
using FlightManagement.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class FlightsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FlightsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> All()
    {
        var result = await _mediator.Send(new GetAllFlightsQuery());
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Entity);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> AllFromDepartureAndDestinationCities([FromQuery] string departureCity,
        [FromQuery] string destinationCity)
    {
        var result = await _mediator.Send(new GetAllFlightsFromDepartureAndDestinationCitiesQuery()
            { DepartureCity = departureCity, DestinationCity = destinationCity });
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Entity);
    }

    [HttpGet("{flightId:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get(Guid flightId)
    {
        var result = await _mediator.Send(new GetFlightQuery() { FlightId = flightId });
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Entity);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateFlightCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Created(nameof(All), result.Entity);
    }
}