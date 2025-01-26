using Microsoft.AspNetCore.Mvc;
using PreschoolApp.Models;
using PreschoolApp.Requests;
using PreschoolApp.Services.Interfaces;

namespace PreschoolApp.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    // Endpoint: Dodawanie opinii
    [HttpPost]
    public async Task<IActionResult> AddReview([FromBody] AddReviewRequest request)
    {
        try
        {
            var review = new Review
            {
                StudentId = request.StudentId,
                UserId = request.UserId,
                Content = request.Content,
            };

            var createdReview = await _reviewService.AddReviewAsync(review);
            return CreatedAtAction(nameof(GetReview), new { id = createdReview.Id }, createdReview);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while adding the review.", details = ex.Message });
        }
    }

    // Endpoint: Pobranie opinii po ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetReview(int id)
    {
        var review = await _reviewService.GetReviewByIdAsync(id);
        if (review == null)
        {
            return NotFound(new { message = "Review not found." });
        }

        return Ok(review);
    }

    // Endpoint: Pobranie opinii dla danego ucznia
    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetReviewsForStudent(int studentId)
    {
        var reviews = await _reviewService.GetReviewsForStudentAsync(studentId);
        return Ok(reviews);
    }
}