using FlightManagement.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AllergiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AllergiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> All()
        {
            var result = await _mediator.Send(new GetAllAllergiesQuery());
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Entity);
        }
    }
}