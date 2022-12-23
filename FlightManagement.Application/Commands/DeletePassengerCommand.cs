using FlightManagement.Domain.Helpers;
using MediatR;

namespace FlightManagement.Application.Commands
{
    public class DeletePassengerCommand : IRequest<Result>
    {
        public Guid PassengerId { get; set; }
    }
}