using FlightManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories;

public class BaggageRepository : Repository<Baggage>
{
    public BaggageRepository(DatabaseContext context) : base(context)
    {
    }

    public override async Task<IReadOnlyCollection<Baggage>> AllAsync()
    {
        return await Context.Baggages.Include(b => b.Passenger)
            .ThenInclude(p => p.Person)
            .ToListAsync();
    }
}