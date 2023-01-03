using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using FlightManagement.Application.Commands;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FlightManagement.Infrastructure.Generics;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace FlightManagement.Application.Handlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<TokenResponse>>
    {
        private readonly IRepository<Person> _personRepository;

        public LoginUserCommandHandler(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Result<TokenResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var persons = await _personRepository.FindAsync(p => p.Email == request.Email);
            var person = persons.FirstOrDefault();
            if (person == null)
            {
                return Result<TokenResponse>.Failure($"Person with email {request.Email} not found");
            }

            return !VerifyPassword(request.Password, person.PasswordHash, person.PasswordSalt)
                ? Result<TokenResponse>.Failure("Email or password are wrong")
                : Result<TokenResponse>.Success(new TokenResponse { Token = CreateToken(person) });
        }

        private static string CreateToken(Person person)
        {
            var claims = new List<Claim>()
            {
		new(ClaimTypes.NameIdentifier, person.Id.ToString()),
                new(ClaimTypes.Name, person.Name),
                new(ClaimTypes.Email, person.Email),
                new(ClaimTypes.Surname, person.Surname),
                new(ClaimTypes.Role, person.Role)
            };

            var key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes("FlightManagementTopSecretKey"));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                "https://localhost:44340/",
                "https://localhost:44340/",
                claims: claims,
                expires: DateTime.Now.AddDays(1), signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}