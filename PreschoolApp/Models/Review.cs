using System.ComponentModel.DataAnnotations;

namespace PreschoolApp.Models;

public class Review
{
    public int Id { get; set; }

    [Required]
    public int StudentId { get; set; }
    public Student Student { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; }

    [Required]
    [MaxLength(500)]
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}