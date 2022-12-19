using FlightManagement.Domain.Entities;
using FluentValidation;

namespace FlightManagement.Application.Validator
{
    public class FlightValidator : AbstractValidator<Flight>
    {
        public FlightValidator()
        {
            RuleFor(flight => flight.DepartureDate).NotNull();
            RuleFor(flight => flight.ArrivalDate).NotNull();
            RuleFor(flight => flight.PassengerCapacity).NotNull();
            RuleFor(flight => flight.PassengerCapacity).GreaterThan(0);
            RuleFor(flight => flight.BaggageWeightCapacity).NotNull();
            RuleFor(flight => flight.BaggageWeightCapacity).GreaterThan(0);
            RuleFor(flight => flight.MaxBaggageHeight).NotNull();
            RuleFor(flight => flight.MaxBaggageWeightPerPassenger).NotNull();
            RuleFor(flight => flight.MaxBaggageWeightPerPassenger).GreaterThan(0);
            RuleFor(flight => flight.MaxBaggageHeight).NotNull();
            RuleFor(flight => flight.MaxBaggageHeight).GreaterThan(0);
            RuleFor(flight => flight.MaxBaggageWidth).NotNull();
            RuleFor(flight => flight.MaxBaggageWidth).GreaterThan(0);
            RuleFor(flight => flight.MaxBaggageLength).NotNull();
            RuleFor(flight => flight.MaxBaggageLength).GreaterThan(0);
            RuleFor(flight => flight.DepartureAirport).NotNull();
            RuleFor(flight => flight.DestinationAirport).NotNull();
        }
    }
}