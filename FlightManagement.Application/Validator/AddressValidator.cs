using FlightManagement.Domain.Entities;
using FluentValidation;

namespace FlightManagement.Application.Validator
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(address => address.Number).NotNull();
            RuleFor(address => address.Street).NotNull();
            RuleFor(address => address.City).NotNull();
            RuleFor(address => address.Country).NotNull();
        }
    }
}