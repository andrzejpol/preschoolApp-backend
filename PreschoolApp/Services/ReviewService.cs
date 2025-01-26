using Microsoft.EntityFrameworkCore;
using PreschoolApp.Data;
using PreschoolApp.Models;
using PreschoolApp.Services.Interfaces;

namespace PreschoolApp.Services;

public class ReviewService : IReviewService
{
    private readonly PreschoolDbContext _context;

    public ReviewService(PreschoolDbContext context)
    {
        _context = context;
    }

    // Dodanie opinii
    public async Task<Review> AddReviewAsync(Review review)
    {
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
        return review;
    }

    // Pobranie opinii po ID
    public async Task<Review> GetReviewByIdAsync(int id)
    {
        return await _context.Reviews
            .Include(r => r.Student)
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    // Pobranie opinii dla ucznia
    public async Task<List<Review>> GetReviewsForStudentAsync(int studentId)
    {
        return await _context.Reviews
            .Include(r => r.User) // Dołączamy informacje o użytkowniku, który dodał opinię
            .Where(r => r.StudentId == studentId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }
}