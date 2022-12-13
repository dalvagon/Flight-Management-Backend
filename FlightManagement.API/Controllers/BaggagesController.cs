using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class BaggagesController : ControllerBase
{
    private readonly IRepository<Baggage> _baggageRepository;

    public BaggagesController(
        IRepository<Baggage> baggageRepository
    )
    {
        _baggageRepository = baggageRepository;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        return Ok(await _baggageRepository.AllAsync());
    }
}