using FlightManagement.Application.Responses;
using MediatR;

namespace FlightManagement.Application.Commands;

public class CreateAddressCommand : IRequest<AddressResponse>
{
    public string Number { get; set; }
    public string Street { get; set; }
    public Guid CityId { get; set; }
    public Guid CountryId { get; set; }
}