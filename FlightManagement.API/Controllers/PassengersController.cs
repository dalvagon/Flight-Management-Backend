using FlightManagement.Application.Commands;
using FlightManagement.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class PassengersController : ControllerBase
{
    private readonly IMediator _mediator;

    public PassengersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AllForFlight([FromQuery] Guid flightId)
    {
        var result = await _mediator.Send(new GetAllPassengersForFlightQuery() { FlightId = flightId });
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Entity);
    }

    [HttpGet("{passengerId:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get(Guid passengerId)
    {
        var result = await _mediator.Send(new GetPassengerQuery() { PassengerId = passengerId });
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Entity);
    }

    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Create([FromBody] CreatePassengerCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Created(nameof(Get), result.Entity);
    }

    [HttpDelete("{passengerId:guid}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> Delete(Guid passengerId)
    {
        var result = await _mediator.Send(new DeletePassengerCommand() { PassengerId = passengerId });
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }
}