namespace PreschoolApp.Models;

public class Event
{
    public int EventId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Group { get; set; }
    public DateTime EventDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int CreatedBy { get; set; }
    public User CreatedByUser { get; set; }
}