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
    public class EventsControllerTests
    {
        private Mock<IEventService> _mockEventService;
        private EventsController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockEventService = new Mock<IEventService>();
            _controller = new EventsController(_mockEventService.Object);
        }

        [Test]
        public async Task GetAllEvents_ShouldReturnOkWithEvents()
        {
            // Arrange
            var mockEvents = new List<Event>
            {
                new Event { EventId = 1, Title = "Event 1", Description = "Description 1" },
                new Event { EventId = 2, Title = "Event 2", Description = "Description 2" }
            };

            _mockEventService.Setup(service => service.GetAllEventsAsync())
                .ReturnsAsync(mockEvents);

            // Act
            var result = await _controller.GetAllEvents();

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo(mockEvents));
        }
    }
}