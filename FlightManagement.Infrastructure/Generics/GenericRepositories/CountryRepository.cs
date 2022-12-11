using FlightManagement.Domain.Entities;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories
{
    public class CountryRepository : Repository<Country>
    {
        public CountryRepository(DatabaseContext context) : base(context)
        {
        }
    }
}