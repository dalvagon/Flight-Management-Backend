using FlightManagement.Domain.Entities;
using FluentValidation;

namespace FlightManagement.Application.Validator
{
    public class AirportValidator : AbstractValidator<Airport>
    {
        public AirportValidator()
        {
            RuleFor(airport => airport.Name).NotNull();
            RuleFor(airport => airport.Address).NotNull();
        }
    }
}