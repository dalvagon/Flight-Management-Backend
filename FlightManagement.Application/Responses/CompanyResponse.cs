using FlightManagement.Domain.Entities;

namespace FlightManagement.Application.Responses;

public class CompanyResponse
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public DateTime CreationDate { get; private set; }
    public List<Administrator> Administrators { get; private set; }
}