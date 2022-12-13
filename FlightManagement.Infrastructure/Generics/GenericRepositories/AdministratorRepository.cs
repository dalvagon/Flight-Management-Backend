using FlightManagement.Domain.Entities;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories;

public class AdministratorRepository : Repository<Administrator>
{
    public AdministratorRepository(DatabaseContext context) : base(context)
    {
    }
}