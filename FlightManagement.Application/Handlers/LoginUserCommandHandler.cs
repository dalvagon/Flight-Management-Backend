using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using FlightManagement.Application.Commands;
using FlightManagement.Application.Responses;
using FlightManagement.Domain.Entities;
using FlightManagement.Domain.Helpers;
using FlightManagement.Infrastructure.Generics;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FlightManagement.Application.Handlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<TokenResponse>>
    {
        private readonly IRepository<Person> _personRepository;
        private readonly IConfiguration _config;

        public LoginUserCommandHandler(IRepository<Person> personRepository, IConfiguration config)
        {
            _personRepository = personRepository;
            _config = config;
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
                : Result<TokenResponse>.Success(new TokenResponse() { Token = CreateToken(person) });
        }

        private string CreateToken(Person person)
        {
            var claims = new List<Claim>()
            {
                new(ClaimTypes.Name, person.Name),
                new(ClaimTypes.Email, person.Email),
                new(ClaimTypes.Surname, person.Surname),
                new(ClaimTypes.Role, person.Role)
            };

            var key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                _config.GetSection("Jwt:Issuer").Value!,
                _config.GetSection("Jwt:Audience").Value!,
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