using FlightManagement.Application.Commands;
using FluentValidation;

namespace FlightManagement.Application.Validators
{
    public class CreateAirportCommandValidator : AbstractValidator<CreateAirportCommand>
    {
        public CreateAirportCommandValidator()
        {
            RuleFor(airport => airport.Name).NotEmpty().NotNull();
            RuleFor(airport => airport.Address).NotEmpty().NotNull();
        }
    }
}