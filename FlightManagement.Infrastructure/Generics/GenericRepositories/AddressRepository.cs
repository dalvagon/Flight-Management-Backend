using FlightManagement.Domain.Entities;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories
{
    public class AddressRepository : Repository<Address>
    {
        public AddressRepository(DatabaseContext context) : base(context)
        {
        }
    }
}