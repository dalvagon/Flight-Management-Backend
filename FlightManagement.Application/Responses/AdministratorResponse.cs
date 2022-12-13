using FlightManagement.Domain.Entities;

namespace FlightManagement.Application.Responses;

public class AdministratorResponse
{
    public Guid Id { get; private set; }
    public Company Company { get; private set; }
    public Person Person { get; private set; }
}