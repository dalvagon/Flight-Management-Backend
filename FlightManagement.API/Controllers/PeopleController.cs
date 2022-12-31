using FlightManagement.Application.Commands;
using FlightManagement.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class PeopleController : ControllerBase
{
    private readonly IMediator _mediator;

    public PeopleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> All()
    {
        var result = await _mediator.Send(new GetAllPeopleQuery());
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Entity);
    }

    [HttpGet("{personId:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get(Guid personId)
    {
        var result = await _mediator.Send(new GetPersonQuery() { PersonId = personId });
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Entity);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Created(nameof(Get), result.Entity);
    }


    [HttpDelete("{personId:guid}")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> Delete(Guid personId)
    {
        var result = await _mediator.Send(new DeletePersonCommand() { PersonId = personId });
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }
}