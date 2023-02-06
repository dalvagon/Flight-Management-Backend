using FlightManagement.Application.Responses;
using FlightManagement.Domain.Helpers;
using MediatR;

namespace FlightManagement.Application.Commands
{
    public class LoginUserCommand : IRequest<Result<TokenResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}