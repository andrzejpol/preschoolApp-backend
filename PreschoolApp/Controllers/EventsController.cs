using Microsoft.AspNetCore.Mvc;
using PreschoolApp.Models;
using PreschoolApp.Services.Interfaces;

namespace PreschoolApp.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        var events = await _eventService.GetAllEventsAsync();
        return Ok(events);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEvent(int id)
    {
        var eventItem = await _eventService.GetEventByIdAsync(id);
        if (eventItem == null) return NotFound();
        return Ok(eventItem);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] Event newEvent)
    {
        var createdEvent = await _eventService.CreateEventAsync(newEvent);
        return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.EventId }, createdEvent);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(int id, [FromBody] Event updatedEvent)
    {
        var success = await _eventService.UpdateEventAsync(id, updatedEvent);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var success = await _eventService.DeleteEventAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}