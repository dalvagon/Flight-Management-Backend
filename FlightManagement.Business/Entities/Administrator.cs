namespace FlightManagement.Business.Entities
{
    public class Administrator : Person
    {
        public Guid CompanyId { get; private set; }
    }
}
