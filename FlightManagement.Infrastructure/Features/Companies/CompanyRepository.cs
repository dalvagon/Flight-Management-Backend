using FlightManagement.API.Data;
using FlightManagement.Business.Entities;

namespace FlightManagement.API.Features.Companies
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DatabaseContext context = new DatabaseContext();

        public void Add(Company company)
        {
            context.Set<Company>().Add(company);
        }
        public IEnumerable<Company> GetAll()
        {
            return context.Set<Company>().ToList();
        }

    }
}
