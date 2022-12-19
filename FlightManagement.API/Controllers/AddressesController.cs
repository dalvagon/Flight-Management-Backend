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
        var addresses = await _mediator.Send(new GetAllAddressesQuery());

        return Ok(addresses);
    }

    [HttpGet("{addressId:guid}")]
    public async Task<IActionResult> Get(Guid addressId)
    {
        var address = await _mediator.Send(new GetAddressCommand() { AddressId = addressId });

        return Ok(address);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAddressCommand command)
    {
        var address = await _mediator.Send(command);

        return Created(nameof(Get), address);
    }

    [HttpDelete("{addressId:guid}")]
    public async Task<IActionResult> Delete(Guid addressId)
    {
        await _mediator.Send(new DeleteAddressCommand() { AddressId = addressId });

        return NoContent();
    }
}