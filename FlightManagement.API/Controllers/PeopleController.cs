using FlightManagement.API.Features.Companies;
using FlightManagement.API.Features.Persons;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers
{
    [Route("api/v1/people")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonRepository personRepository;

        public PeopleController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        [HttpGet]
        public IActionResult GetALl()
        {
            return Ok(personRepository.GetAll());
        }
    }
}
