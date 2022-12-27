using FlightManagement.Application.Commands;
using FluentValidation;

namespace FlightManagement.Application.Validators
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(person => person.Name).NotNull();
            RuleFor(person => person.Surname).NotNull();
            RuleFor(person => person.Email).NotNull();
            RuleFor(person => person.Password).NotNull();
            RuleFor(person => person.DateOfBirth).NotNull();
            RuleFor(person => person.Gender).NotNull();
            RuleFor(person => person.Address).NotNull();
            RuleFor(person => person.Address.Number).NotNull();
            RuleFor(person => person.Address.Street).NotNull();
            RuleFor(person => person.Address.CityId).NotNull();
            RuleFor(person => person.Address.CountryId).NotNull();
        }
    }
}