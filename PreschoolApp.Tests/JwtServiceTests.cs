using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using NUnit.Framework;
using PreschoolApp.Services;

namespace PreschoolApp.Tests
{
    public class JwtServiceTests
    {
        private JwtService _jwtService;
        private const string TestKey = "ThisIsASecretKeyForTestingOnly123!";

        [SetUp]
        public void SetUp()
        {
            _jwtService = new JwtService(TestKey);
        }

        [Test]
        public void GenerateToken_ShouldReturnValidToken()
        {
            // Arrange
            var email = "test@example.com";
            var role = "Admin";

            // Act
            var token = _jwtService.GenerateToken(email, role);

            // Assert
            Assert.That(token, Is.Not.Null);
            Assert.That(token, Is.Not.Empty);

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            Assert.That(jwtToken, Is.Not.Null);
            Assert.That(jwtToken.Claims, Has.Some.Matches<Claim>(c => c.Type == ClaimTypes.Email && c.Value == email));
            Assert.That(jwtToken.Claims, Has.Some.Matches<Claim>(c => c.Type == ClaimTypes.Role && c.Value == role));
        }
    }
}