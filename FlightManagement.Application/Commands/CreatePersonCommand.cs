using FlightManagement.Application.Responses;
using FlightManagement.Domain.Helpers;
using MediatR;

namespace FlightManagement.Application.Commands
{
    public class CreatePersonCommand : IRequest<Result<PersonResponse>>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public CreateAddressCommand Address { get; set; }
    }
}