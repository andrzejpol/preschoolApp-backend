using Microsoft.EntityFrameworkCore;
using PreschoolApp.Data;
using PreschoolApp.Models;
using PreschoolApp.Services.Interfaces;

namespace PreschoolApp.Services;

public class EventService : IEventService
{
    private readonly PreschoolDbContext _applicationDbContext;

    public EventService(PreschoolDbContext dbContext)
    {
        _applicationDbContext = dbContext;
    }

    public async Task<List<Event>> GetAllEventsAsync()
    {
        return await _applicationDbContext.Events.ToListAsync();
    }

    public async Task<Event> GetEventByIdAsync(int id)
    {
        return await _applicationDbContext.Events.FindAsync(id);
    }

    public async Task<Event> CreateEventAsync(Event newEvent)
    {
        _applicationDbContext.Events.Add(newEvent);
        await _applicationDbContext.SaveChangesAsync();
        return newEvent;
    }
    
    public async Task<bool> UpdateEventAsync(int id, Event updatedEvent)
    {
        var existingEvent = await _applicationDbContext.Events.FindAsync(id);
        if (existingEvent == null) return false;

        existingEvent.Title = updatedEvent.Title;
        existingEvent.Description = updatedEvent.Description;
        existingEvent.Group = updatedEvent.Group;
        existingEvent.EventDate = updatedEvent.EventDate;
        existingEvent.StartTime = updatedEvent.StartTime;
        existingEvent.EndTime = updatedEvent.EndTime;

        await _applicationDbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> DeleteEventAsync(int id)
    {
        var eventToDelete = await _applicationDbContext.Events.FindAsync(id);
        if (eventToDelete == null) return false;

        _applicationDbContext.Events.Remove(eventToDelete);
        await _applicationDbContext.SaveChangesAsync();
        return true;
    }
}