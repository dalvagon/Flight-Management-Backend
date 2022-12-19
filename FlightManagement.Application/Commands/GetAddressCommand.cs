using FlightManagement.Application.Responses;
using MediatR;

namespace FlightManagement.Application.Commands
{
    public class GetAddressCommand : IRequest<AddressResponse>
    {
        public Guid AddressId { get; set; }
    }
}