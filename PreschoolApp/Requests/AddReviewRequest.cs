using System.ComponentModel.DataAnnotations;

namespace PreschoolApp.Requests;

public class AddReviewRequest
{
    [Required]
    public int StudentId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    [MaxLength(500)]
    public string Content { get; set; }
}