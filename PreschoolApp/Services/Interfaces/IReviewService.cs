using PreschoolApp.Models;

namespace PreschoolApp.Services.Interfaces;

public interface IReviewService
{
    Task<Review> AddReviewAsync(Review review);
    Task<Review> GetReviewByIdAsync(int id);
    Task<List<Review>> GetReviewsForStudentAsync(int studentId);
}