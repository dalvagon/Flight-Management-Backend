
using FlightManagement.Business.Entities;

namespace FlightManagement.API.Features.Companies
{
    public interface ICompanyRepository
    {
        void Add(Company company);
        IEnumerable<Company> GetAll();
    }
}