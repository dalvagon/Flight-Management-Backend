using FlightManagement.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class CountriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CountriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var result = await _mediator.Send(new GetAllCountriesQuery());
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        var countries = result.Entity;

        return Ok(countries);
    }
}