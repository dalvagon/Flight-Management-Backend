using FlightManagement.Business.Entities;
using FlightManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers
{
    [Route("api/v1/people")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IRepository<Person> personRepository;

        public PeopleController(IRepository<Person> personRepository)
        {
            this.personRepository = personRepository;
        }

        [HttpGet]
        public IActionResult All()
        {
            return Ok(personRepository.All());
        }
    }
}
