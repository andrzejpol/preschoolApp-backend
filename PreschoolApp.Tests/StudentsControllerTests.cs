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
    public class StudentsControllerTests
    {
        private Mock<IStudentService> _mockStudentService;
        private StudentsController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockStudentService = new Mock<IStudentService>();
            _controller = new StudentsController(_mockStudentService.Object);
        }

        [Test]
        public async Task GetAllStudents_ShouldReturnOkWithStudents()
        {
            // Arrange
            var mockStudents = new List<Student>
            {
                new Student { StudentId = 1, FirstName = "John", LastName = "Doe" },
                new Student { StudentId = 2, FirstName = "Jane", LastName = "Smith" }
            };

            _mockStudentService.Setup(service => service.GetAllStudentsAsync())
                .ReturnsAsync(mockStudents);

            // Act
            var result = await _controller.GetAllStudents();

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo(mockStudents));
        }
    }
}