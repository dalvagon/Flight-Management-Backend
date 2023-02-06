using FlightManagement.Application.Responses;
using FlightManagement.Domain.Helpers;
using MediatR;

namespace FlightManagement.Application.Commands;

public class CreateAddressCommand : IRequest<Result<AddressResponse>>
{
    public string Number { get; set; }
    public string Street { get; set; }
    public Guid CityId { get; set; }
    public Guid CountryId { get; set; }
}