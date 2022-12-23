using FlightManagement.Application.Commands;
using FlightManagement.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class AddressesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AddressesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var result = await _mediator.Send(new GetAllAddressesQuery());

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Entity);
    }

    [HttpGet("{addressId:guid}")]
    public async Task<IActionResult> Get(Guid addressId)
    {
        var result = await _mediator.Send(new GetAddressQuery() { AddressId = addressId });
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Entity);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAddressCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Created(nameof(Get), result.Entity);
    }

    [HttpDelete("{addressId:guid}")]
    public async Task<IActionResult> Delete(Guid addressId)
    {
        var result = await _mediator.Send(new DeleteAddressCommand() { AddressId = addressId });

        if (result.IsFailure)
        {
            return BadRequest("Couldn't delete address");
        }

        return NoContent();
    }
}