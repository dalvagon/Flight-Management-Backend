using FlightManagement.Application.Responses;
using FlightManagement.Domain.Helpers;
using MediatR;

namespace FlightManagement.Application.Commands
{
    public class CreatePassengerCommand : IRequest<Result<PassengerResponse>>
    {
        public Guid PersonId { get; set; }
        public Guid FlightId { get; set; }
        public double Weight { get; set; }
        public List<CreateBaggageCommand> Baggages { get; set; } = new List<CreateBaggageCommand>();
        public List<Guid> AllergyIds { get; set; } = new List<Guid>();
    }
}