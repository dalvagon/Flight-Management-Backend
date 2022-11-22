namespace FlightManagement.Domain.Entities
{
    public class Allergy
    {
        public Guid Id { get; private set; }
        public String Name { get; private set; }

        public Allergy(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
