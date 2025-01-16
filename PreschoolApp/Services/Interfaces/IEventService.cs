using PreschoolApp.Models;

namespace PreschoolApp.Services.Interfaces;

public interface IEventService
{
    Task<List<Event>> GetAllEventsAsync();
    Task<Event> GetEventByIdAsync(int id);
    Task<Event> CreateEventAsync(Event newEvent);
    Task<bool> UpdateEventAsync(int id, Event updatedEvent);
    Task<bool> DeleteEventAsync(int id);
}