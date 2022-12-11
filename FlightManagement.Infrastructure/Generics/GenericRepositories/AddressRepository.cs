using FlightManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories
{
    public class AddressRepository : Repository<Address>
    {
        public AddressRepository(DatabaseContext context) : base(context)
        {
        }

        public override Task<Address> GetAsync(Guid id)
        {
            return Context.Addresses
                .Include(a => a.Country)
                .Include(a => a.City)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public override async Task<IReadOnlyCollection<Address>> AllAsync()
        {
            return await Context.Addresses.Include(a => a.Country)
                .Include(a => a.City)
                .ToListAsync();
        }
    }
}