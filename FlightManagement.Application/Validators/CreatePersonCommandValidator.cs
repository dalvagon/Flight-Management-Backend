using FlightManagement.Application.Commands;
using FluentValidation;

namespace FlightManagement.Application.Validators
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(person => person.Name).NotEmpty();
            RuleFor(person => person.Surname).NotEmpty();
            RuleFor(person => person.Email).NotEmpty();
            RuleFor(person => person.Password).NotEmpty();
            RuleFor(person => person.DateOfBirth).NotNull();
            RuleFor(person => person.Gender).NotEmpty();
            RuleFor(person => person.Address).NotNull();
            RuleFor(person => person.Address.Number).NotEmpty();
            RuleFor(person => person.Address.Street).NotEmpty();
            RuleFor(person => person.Address.CityId).NotEmpty();
            RuleFor(person => person.Address.CountryId).NotEmpty();
        }
    }
}