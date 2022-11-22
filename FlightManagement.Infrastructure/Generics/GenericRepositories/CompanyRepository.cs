using FlightManagement.Business.Entities;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories
{
    public class CompanyRepository : Repository<Company>
    {
        public CompanyRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
