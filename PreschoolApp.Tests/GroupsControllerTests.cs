using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PreschoolApp.Controllers;
using PreschoolApp.Models;
using PreschoolApp.Services.Interfaces;

namespace PreschoolApp.Tests
{
    public class GroupsControllerTests
    {
        private Mock<IGroupService> _mockGroupService;
        private GroupsController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockGroupService = new Mock<IGroupService>();
            _controller = new GroupsController(_mockGroupService.Object);
        }

        [Test]
        public async Task GetAllGroups_ShouldReturnOkWithGroups()
        {
            // Arrange
            var mockGroups = new List<Group>
            {
                new Group { Id = 1, Name = "Group A" },
                new Group { Id = 2, Name = "Group B" }
            };

            _mockGroupService.Setup(service => service.GetAllGroupsAsync())
                .ReturnsAsync(mockGroups);

            // Act
            var result = await _controller.GetAllGroups();

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo(mockGroups));
        }
    }
}