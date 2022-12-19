using MediatR;

namespace FlightManagement.Application.Commands
{
    public class DeleteAddressCommand : IRequest
    {
        public Guid AddressId { get; set; }
    }
}