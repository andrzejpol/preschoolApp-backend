using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using PreschoolApp.Configuration;
using PreschoolApp.Data;
using PreschoolApp.Models;
using PreschoolApp.Requests;
using PreschoolApp.Services;
using PreschoolApp.Tools.Interfaces;

namespace PreschoolApp.Tests
{
    public class UserServiceTests
    {
        private PreschoolDbContext _dbContext;
        private UserService _userService;
        private IPasswordHasher _mockPasswordHasher;
        private AuthenticationConfiguration _authConfig;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<PreschoolDbContext>()
                .UseInMemoryDatabase(databaseName: $"PreschoolDb_{System.Guid.NewGuid()}")
                .Options;

            _dbContext = new PreschoolDbContext(options);

            _mockPasswordHasher = NSubstitute.Substitute.For<IPasswordHasher>();
            _authConfig = new AuthenticationConfiguration { SaltSize = 16 };

            _userService = new UserService(_mockPasswordHasher, _dbContext, _authConfig);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public async Task GetUsersAsync_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User
                {
                    Email = "user1@example.com",
                    FirstName = "User1",
                    LastName = "Test",
                    PasswordHash = "hash1",
                    Salt = new byte[] { 1, 2, 3 },
                    Role = User.UserRole.Parent
                },
                new User
                {
                    Email = "user2@example.com",
                    FirstName = "User2",
                    LastName = "Test",
                    PasswordHash = "hash2",
                    Salt = new byte[] { 4, 5, 6 },
                    Role = User.UserRole.Teacher
                }
            };

            _dbContext.Users.AddRange(users);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _userService.GetUsersAsync();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Email, Is.EqualTo("user1@example.com"));
            Assert.That(result[0].Role, Is.EqualTo(User.UserRole.Parent));
            Assert.That(result[1].Email, Is.EqualTo("user2@example.com"));
            Assert.That(result[1].Role, Is.EqualTo(User.UserRole.Teacher));
        }
    }
}
