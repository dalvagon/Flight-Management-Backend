namespace FlightManagement.Business.Entities
{
    public class Administrator : Person
    {
        public Guid Id { get; private set; }
        public Company Company { get; private set; }
        public Person Person { get; private set; }

        public Administrator(Company company, Person person)
        {
            Id = Guid.NewGuid();
            Company = company;
            Person = person;
        }
    }
}
