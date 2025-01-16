using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PreschoolApp.Configuration;
using PreschoolApp.Models;
using PreschoolApp.Services.Interfaces;
using PreschoolApp.Tools.Interfaces;

namespace PreschoolApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly AuthenticationConfiguration _authenticationConfiguration;

        public AuthService(AuthenticationConfiguration authenticationConfiguration,
            IPasswordHasher passwordHasher)
        {
            _authenticationConfiguration = authenticationConfiguration;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> GenerateTokenAsync(User user, string password)
        {
            if (!_passwordHasher.Compare(password, user.PasswordHash, user.Salt))
            {
                throw new BadHttpRequestException("Invalid user login or password");
            }

            Claim[] claims = new Claim[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("Role", user.Role.ToString()),

            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationConfiguration.JwtKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            DateTime expires = DateTime.Now.AddDays(_authenticationConfiguration.JwtExpireDays);

            JwtSecurityToken token = new JwtSecurityToken(_authenticationConfiguration.JwtIssuer,
                _authenticationConfiguration.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: credentials);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}