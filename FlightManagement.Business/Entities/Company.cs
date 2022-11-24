namespace FlightManagement.Domain.Entities
{
    public class Company
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateTime CreationDate { get; private set; }
        public List<Administrator> Administrators { get; private set; }

        public Company(string name, DateTime creationDate)
        {
            Id = Guid.NewGuid();
            Name = name;
            CreationDate = creationDate;
        }
    }
}
