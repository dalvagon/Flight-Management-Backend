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
        private readonly IRepository<Address> _addressRepository;

        public PeopleController(IRepository<Person> personRepository, IRepository<Address> addressRepository)
        {
            _personRepository = personRepository;
            _addressRepository = addressRepository;
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
            var address = new Address(dto.AddressDto.Number, dto.AddressDto.Street, dto.AddressDto.City,
                dto.AddressDto.Country);
            var result = Person.Create(dto.Name, dto.Surname, dto.DateOfBirth, dto.Gender, address);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            Person person = result.Entity;
            _addressRepository.Add(address);
            _addressRepository.SaveChanges();
            _personRepository.Add(person);
            _personRepository.SaveChanges();

            return Created(nameof(Get), person);
        }

        [HttpDelete("{personId:guid}")]
        public IActionResult Delete(Guid personId)
        {
            var person = _personRepository.Get(personId);

            _personRepository.Delete(person);
            _personRepository.SaveChanges();

            return NoContent();
        }
    }
}