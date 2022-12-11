using FlightManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories
{
    public class AirportRepository : Repository<Airport>
    {
        public AirportRepository(DatabaseContext context) : base(context)
        {
        }

        public override Task<Airport> GetAsync(Guid id)
        {
            return Context.Airports
                .Include(airport => airport.Address)
                .ThenInclude(a => a.Country)
                .Include(a => a.Address)
                .ThenInclude(a => a.City)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public override async Task<IReadOnlyCollection<Airport>> AllAsync()
        {
            return await Context.Airports
                .Include(airport => airport.Address)
                .ThenInclude(a => a.Country)
                .Include(a => a.Address)
                .ThenInclude(a => a.City)
                .ToListAsync();
        }
    }
}