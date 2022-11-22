using FlightManagement.Business.Entities;
using FlightManagement.Infrastructure.Generics;
using Microsoft.AspNetCore.Mvc;

namespace FlightManagement.API.Controllers
{
    [Route("api/v1/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IRepository<Company> companyRepository;

        public CompanyController(IRepository<Company> companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        [HttpGet]
        public IActionResult All()
        {
            return Ok(companyRepository.All());
        }
    }
}
