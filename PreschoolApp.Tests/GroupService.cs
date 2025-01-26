using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PreschoolApp.Data;
using PreschoolApp.Models;
using PreschoolApp.Services;

namespace PreschoolApp.Tests
{
    public class GroupServiceTests
    {
        private PreschoolDbContext _dbContext;
        private GroupService _groupService;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<PreschoolDbContext>()
                .UseInMemoryDatabase(databaseName: $"PreschoolDb_{System.Guid.NewGuid()}")
                .Options;

            _dbContext = new PreschoolDbContext(options);
            _groupService = new GroupService(_dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public async Task GetAllGroupsAsync_ShouldReturnAllGroups()
        {
            // Arrange
            var groups = new List<Group>
            {
                new Group { Id = 1, Name = "Group A" },
                new Group { Id = 2, Name = "Group B" }
            };

            _dbContext.Groups.AddRange(groups);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _groupService.GetAllGroupsAsync();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Name, Is.EqualTo("Group A"));
            Assert.That(result[1].Name, Is.EqualTo("Group B"));
        }

        [Test]
        public async Task CreateGroupAsync_ShouldAddGroupToDatabase()
        {
            // Arrange
            var newGroup = new Group
            {
                Name = "Group C"
            };

            // Act
            var result = await _groupService.CreateGroupAsync(newGroup);
            var groupsInDb = await _dbContext.Groups.ToListAsync();

            // Assert
            Assert.That(groupsInDb.Count, Is.EqualTo(1));
            Assert.That(groupsInDb[0].Name, Is.EqualTo("Group C"));
            Assert.That(result, Is.EqualTo(groupsInDb[0]));
        }
    }
}
