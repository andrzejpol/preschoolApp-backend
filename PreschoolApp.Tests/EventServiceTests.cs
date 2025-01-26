using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PreschoolApp.Data;
using PreschoolApp.Models;
using PreschoolApp.Services;

namespace PreschoolApp.Tests
{
    public class EventServiceTests
    {
        private PreschoolDbContext _dbContext;
        private EventService _eventService;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<PreschoolDbContext>()
                .UseInMemoryDatabase(databaseName: $"PreschoolDb_{Guid.NewGuid()}")
                .Options;

            _dbContext = new PreschoolDbContext(options);
            _eventService = new EventService(_dbContext);
        }


        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public async Task GetAllEventsAsync_ShouldReturnAllEvents()
        {
            // Arrange
            var events = new List<Event>
            {
                new Event { EventId = 1, Title = "Event 1", Description = "Description 1", Group = "A" },
                new Event { EventId = 2, Title = "Event 2", Description = "Description 2", Group = "A" }
            };
            _dbContext.Events.AddRange(events);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _eventService.GetAllEventsAsync();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Title, Is.EqualTo("Event 1"));
            Assert.That(result[1].Title, Is.EqualTo("Event 2"));
        }

        [Test]
        public async Task CreateEventAsync_ShouldAddEventToDatabase()
        {
            // Arrange
            var newEvent = new Event
            {
                Title = "New Event",
                Description = "New Description",
                Group = "Group A",
                EventDate = System.DateTime.Now,
                StartTime = System.TimeSpan.FromHours(9),
                EndTime = System.TimeSpan.FromHours(10)
            };

            // Act
            var result = await _eventService.CreateEventAsync(newEvent);
            var eventsInDb = await _dbContext.Events.ToListAsync();

            // Assert
            Assert.That(eventsInDb.Count, Is.EqualTo(1));
            Assert.That(eventsInDb[0].Title, Is.EqualTo("New Event"));
            Assert.That(result, Is.EqualTo(eventsInDb[0]));
        }
    }
}
