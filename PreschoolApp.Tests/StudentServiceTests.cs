using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PreschoolApp.Data;
using PreschoolApp.Models;
using PreschoolApp.Services;

namespace PreschoolApp.Tests
{
    public class StudentServiceTests
    {
        private PreschoolDbContext _dbContext;
        private StudentService _studentService;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<PreschoolDbContext>()
                .UseInMemoryDatabase(databaseName: $"PreschoolDb_{System.Guid.NewGuid()}")
                .Options;

            _dbContext = new PreschoolDbContext(options);
            _studentService = new StudentService(_dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public async Task GetAllStudentsAsync_ShouldReturnAllStudents()
        {
            // Arrange
            var parent = new User
            {
                FirstName = "ParentFirstName",
                LastName = "ParentLastName",
                Email = "parent@example.com",
                PasswordHash = "hashedpassword",
                Salt = new byte[] { 1, 2, 3 },
                Role = User.UserRole.Parent
            };

            _dbContext.Users.Add(parent);
            await _dbContext.SaveChangesAsync();

            var students = new List<Student>
            {
                new Student
                {
                    StudentId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = DateTime.Now.AddYears(-10),
                    ParentId = parent.Id,
                    Parent = parent
                },
                new Student
                {
                    StudentId = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    DateOfBirth = DateTime.Now.AddYears(-8),
                    ParentId = parent.Id,
                    Parent = parent
                }
            };

            _dbContext.Students.AddRange(students);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _studentService.GetAllStudentsAsync();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].FirstName, Is.EqualTo("John"));
            Assert.That(result[1].FirstName, Is.EqualTo("Jane"));
            Assert.That(result[0].Parent.Email, Is.EqualTo("parent@example.com"));
        }

        [Test]
        public async Task AddStudentAsync_ShouldAddStudentToDatabase()
        {
            // Arrange
            var parent = new User
            {
                FirstName = "ParentFirstName",
                LastName = "ParentLastName",
                Email = "parent@example.com",
                PasswordHash = "hashedpassword",
                Salt = new byte[] { 1, 2, 3 },
                Role = User.UserRole.Parent
            };

            _dbContext.Users.Add(parent);
            await _dbContext.SaveChangesAsync();

            var newStudent = new Student
            {
                FirstName = "Michael",
                LastName = "Johnson",
                DateOfBirth = DateTime.Now.AddYears(-6),
                ParentId = parent.Id
            };

            // Act
            var result = await _studentService.AddStudentAsync(newStudent);
            var studentsInDb = await _dbContext.Students.ToListAsync();

            // Assert
            Assert.That(studentsInDb.Count, Is.EqualTo(1));
            Assert.That(studentsInDb[0].FirstName, Is.EqualTo("Michael"));
            Assert.That(studentsInDb[0].Parent.Email, Is.EqualTo("parent@example.com"));
            Assert.That(result, Is.EqualTo(studentsInDb[0]));
        }
    }
}
