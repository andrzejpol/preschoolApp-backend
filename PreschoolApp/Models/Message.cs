namespace PreschoolApp.Models;

public class Message : BaseEntity
{
    public int SenderId { get; set; }
    public User Sender { get; set; }

    public int ReceiverId { get; set; }
    public User Receiver { get; set; }

    public string Content { get; set; }
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
}