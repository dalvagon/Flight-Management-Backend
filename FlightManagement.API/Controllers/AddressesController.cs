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

        public AddressesController(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [HttpGet]
        public IActionResult All()
        {
            return Ok(_addressRepository.All());
        }

        [HttpGet("{addressId:guid}")]
        public IActionResult Get(Guid addressId)
        {
            return Ok(_addressRepository.Get(addressId));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateAddressDto dto)
        {
            var address = new Address(dto.Number, dto.Street, dto.City, dto.Country);

            _addressRepository.Add(address);
            _addressRepository.SaveChanges();

            return Created(nameof(Get), address);
        }

        [HttpDelete(("{addressId:guid}"))]
        public IActionResult Delete(Guid addressId)
        {
            var address = _addressRepository.Get(addressId);

            _addressRepository.Delete(address);
            _addressRepository.SaveChanges();

            return NoContent();
        }
    }
}