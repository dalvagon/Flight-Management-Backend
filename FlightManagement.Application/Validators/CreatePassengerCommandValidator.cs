using FlightManagement.Application.Commands;
using FluentValidation;

namespace FlightManagement.Application.Validators
{
    public class CreatePassengerCommandValidator : AbstractValidator<CreatePassengerCommand>
    {
        public CreatePassengerCommandValidator()
        {
            RuleFor(passenger => passenger.PersonId).NotNull();
            RuleFor(passenger => passenger.FlightId).NotNull();
            RuleFor(passenger => passenger.Weight).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}