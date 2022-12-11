using FlightManagement.API.Dtos;
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
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<City> _cityRepository;

        public PeopleController(IRepository<Person> personRepository, IRepository<Address> addressRepository,
            IRepository<Country> countryRepository, IRepository<City> cityRepository)
        {
            _personRepository = personRepository;
            _addressRepository = addressRepository;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return Ok(await _personRepository.AllAsync());
        }

        [HttpGet("{personId:guid}")]
        public async Task<IActionResult> Get(Guid personId)
        {
            return Ok(await _personRepository.GetAsync(personId));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePersonDto dto)
        {
            var country = await _countryRepository.GetAsync(dto.AddressDto.CountryId);
            var city = await _cityRepository.GetAsync(dto.AddressDto.CityId);


            var address = Address.Create(dto.AddressDto.Number, dto.AddressDto.Street, city,
                country).Entity;
            var result = Person.Create(dto.Name, dto.Surname, dto.DateOfBirth, dto.Gender, address);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            Person person = result.Entity;
            await _addressRepository.AddAsync(address);
            _addressRepository.SaveChangesAsync();
            await _personRepository.AddAsync(person);
            _personRepository.SaveChangesAsync();

            return Created(nameof(All), person);
        }

        [HttpDelete("{personId:guid}")]
        public async Task<IActionResult> Delete(Guid personId)
        {
            var person = await _personRepository.GetAsync(personId);

            _personRepository.DeleteAsync(person);
            _personRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}