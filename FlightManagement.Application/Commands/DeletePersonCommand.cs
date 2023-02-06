using FlightManagement.Domain.Helpers;
using MediatR;

namespace FlightManagement.Application.Commands
{
    public class DeletePersonCommand : IRequest<Result>
    {
        public Guid PersonId { get; set; }
    }
}