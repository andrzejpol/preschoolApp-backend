using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PreschoolApp.Controllers;
using PreschoolApp.Requests;
using PreschoolApp.Services.Interfaces;

namespace PreschoolApp.Tests
{
    public class UsersControllerTests
    {
        private Mock<IUserService> _mockUserService;
        private UsersController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockUserService = new Mock<IUserService>();
            _controller = new UsersController(_mockUserService.Object);
        }

        [Test]
        public async Task RegisterUser_ShouldReturnOk()
        {
            // Arrange
            var registerUserRequest = new RegisterUserRequest
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "password123"
            };

            _mockUserService
                .Setup(service => service.RegisterUserAsync(registerUserRequest))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.RegisterUser(registerUserRequest);

            // Assert
            Assert.That(result, Is.InstanceOf<OkResult>());
        }
    }
}