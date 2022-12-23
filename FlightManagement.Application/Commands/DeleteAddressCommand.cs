using FlightManagement.Domain.Helpers;
using MediatR;

namespace FlightManagement.Application.Commands
{
    public class DeleteAddressCommand : IRequest<Result>
    {
        public Guid AddressId { get; set; }
    }
}