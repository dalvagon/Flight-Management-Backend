using FlightManagement.Application.Commands;
using FluentValidation;

namespace FlightManagement.Application.Validators
{
    public class CreateFlightCommandValidator : AbstractValidator<CreateFlightCommand>
    {
        public CreateFlightCommandValidator()
        {
            RuleFor(flight => flight.DepartureDate).NotNull();
            RuleFor(flight => flight.ArrivalDate).NotNull();
            RuleFor(flight => flight.PassengerCapacity).GreaterThan(0).NotNull();
            RuleFor(flight => flight.BaggageWeightCapacity).GreaterThan(0).NotNull();
            RuleFor(flight => flight.MaxWeightPerBaggage).NotEmpty().GreaterThan(0).NotNull();
            RuleFor(flight => flight.MaxBaggageWeightPerPassenger).GreaterThan(0).NotNull();
            RuleFor(flight => flight.MaxBaggageWidth).GreaterThan(0).NotNull();
            RuleFor(flight => flight.MaxBaggageHeight).GreaterThan(0).NotNull();
            RuleFor(flight => flight.MaxBaggageLength).NotEmpty().NotNull();
            RuleFor(flight => flight.DepartureAirportId).NotEmpty().NotNull();
            RuleFor(flight => flight.DestinationAirportId).NotEmpty().NotNull();
        }
    }
}