using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using MediatR;

namespace FlightManagement.Application.Commands;

public class CreateAddressCommand : IRequest<AddressResponse>
{
    public string Number { get; set; }
    public string Street { get; set; }
    public City City { get; set; }
    public Country Country { get; set; }
}