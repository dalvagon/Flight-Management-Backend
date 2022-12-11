namespace FlightManagement.Application.Responses
{
    public class AdministratorResponse
    {
        public Guid Id { get; private set; }
        public CompanyResponse Company { get; private set; }
        public PersonResponse Person { get; private set; }
    }
}