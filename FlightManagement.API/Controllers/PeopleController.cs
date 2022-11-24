using FlightManagement.API.Features.Persons;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IRepository<Person> _personRepository;

        public PeopleController(IRepository<Person> personRepository)
        {
            this._personRepository = personRepository;
        }

        [HttpGet]
        public IActionResult All()
        {
            return Ok(_personRepository.All());
        }

        [HttpGet("{personId:guid}")]
        public IActionResult Get(Guid personId)
        {
            return Ok(_personRepository.Get(personId));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreatePersonDto dto)
        {
            Person person = Person
                .Create(dto.Name, dto.Surname, dto.DateOfBirth, dto.Gender)
                .Entity;

            _personRepository.Add(person);
            _personRepository.SaveChanges();

            return Created(nameof(Get), person);
        }

        [HttpDelete("{personId:guid}")]
        public IActionResult Delete(Guid personId)
        {
            _personRepository.Delete(personId);
            _personRepository.SaveChanges();

            return NoContent();
        }
    }
}