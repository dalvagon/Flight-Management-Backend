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
            RuleFor(airport => airport.Address.CityId).NotEmpty().NotNull();
            RuleFor(airport => airport.Address.CountryId).NotEmpty().NotNull();
            RuleFor(airport => airport.Address.Number).NotEmpty().NotNull();
            RuleFor(airport => airport.Address.Street).NotEmpty().NotNull();
        }
    }
}