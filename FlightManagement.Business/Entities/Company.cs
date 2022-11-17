using FlightManagement.Business.Helpers;

namespace FlightManagement.Business.Entities
{
    public class Company
    {
        public Guid Id { get; private set; }
        public string? Name { get; private set; }
        public DateTime CreationDate { get; private set; }
        public List<Administrator> Administrators { get; private set; }

        public static Result<Company> Create(string name, DateTime creationDate)
        {
            var company = new Company
            {
                Id = Guid.NewGuid(),
                Name = name,
                CreationDate = creationDate,
            };

            return Result<Company>.Success(company);
        }
    }
}
