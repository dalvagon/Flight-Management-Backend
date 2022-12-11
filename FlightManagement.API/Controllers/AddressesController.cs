using FlightManagement.API.Dtos;
using FlightManagement.Domain.Entities;
using FlightManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<City> _cityRepository;

        public AddressesController(IRepository<Address> addressRepository,
            IRepository<Country> countryRepository,
            IRepository<City> cityRepository)
        {
            _addressRepository = addressRepository;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return Ok(await _addressRepository.AllAsync());
        }

        [HttpGet("{addressId:guid}")]
        public async Task<IActionResult> Get(Guid addressId)
        {
            return Ok(await _addressRepository.GetAsync(addressId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateAddressDto dto)
        {
            var country = await _countryRepository.GetAsync(dto.CountryId);
            var city = await _cityRepository.GetAsync(dto.CityId);

            var address = Address.Create(dto.Number, dto.Street, city, country).Entity;

            await _addressRepository.AddAsync(address);
            _addressRepository.SaveChangesAsync();

            return Created(nameof(Get), address);
        }

        [HttpDelete(("{addressId:guid}"))]
        public async Task<IActionResult> Delete(Guid addressId)
        {
            var address = await _addressRepository.GetAsync(addressId);

            _addressRepository.DeleteAsync(address);
            _addressRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}