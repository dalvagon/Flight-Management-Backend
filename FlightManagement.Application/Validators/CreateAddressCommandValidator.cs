using FlightManagement.Application.Commands;
using FluentValidation;

namespace FlightManagement.Application.Validators
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(address => address.Number).NotEmpty().NotNull();
            RuleFor(address => address.Street).NotEmpty().NotNull();
            RuleFor(address => address.CityId).NotEmpty().NotNull();
            RuleFor(address => address.CountryId).NotEmpty().NotNull();
        }
    }
}