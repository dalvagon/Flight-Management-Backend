using System.Text.Json.Serialization;
using FlightManagement.Domain.Helpers;

namespace FlightManagement.Domain.Entities
{
    public class Company
    {
        [JsonInclude] public Guid Id { get; private set; }
        [JsonInclude] public string Name { get; private set; }
        [JsonInclude] public DateTime CreationDate { get; private set; }
        [JsonInclude] public List<Administrator> Administrators { get; private set; }


        public static Result<Company> Create(string name, DateTime creationDate)
        {
            var company = new Company()
            {
                Id = Guid.NewGuid(),
                Name = name,
                CreationDate = creationDate
            };

            return Result<Company>.Success(company);
        }
    }
}