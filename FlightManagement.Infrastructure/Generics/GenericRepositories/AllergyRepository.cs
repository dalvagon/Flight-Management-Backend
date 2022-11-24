using FlightManagement.Domain.Entities;

namespace FlightManagement.Infrastructure.Generics.GenericRepositories
{
    public class AllergyRepository : Repository<Allergy>
    {
        public AllergyRepository(DatabaseContext context) : base(context)
        {
        }
    }
}