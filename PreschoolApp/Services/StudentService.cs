using Microsoft.EntityFrameworkCore;
using PreschoolApp.Data;
using PreschoolApp.Models;
using PreschoolApp.Services.Interfaces;

namespace PreschoolApp.Services;

public class StudentService : IStudentService
{
    private readonly PreschoolDbContext _context;

    public StudentService(PreschoolDbContext context)
    {
        _context = context;
    }

    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await _context.Students
            .Include(s => s.Parent)
            .Include(s => s.StudentGroups)
            .ToListAsync();
    }

    public async Task<Student> GetStudentByIdAsync(int studentId)
    {
        return await _context.Students
            .Include(s => s.Parent)
            .Include(s => s.StudentGroups)
            .FirstOrDefaultAsync(s => s.StudentId == studentId);
    }

    public async Task<Student> AddStudentAsync(Student newStudent)
    {
        _context.Students.Add(newStudent);
        await _context.SaveChangesAsync();
        return newStudent;
    }

    public async Task<bool> UpdateStudentAsync(int studentId, Student updatedStudent)
    {
        var existingStudent = await _context.Students.FindAsync(studentId);
        if (existingStudent == null) return false;

        existingStudent.FirstName = updatedStudent.FirstName;
        existingStudent.LastName = updatedStudent.LastName;
        existingStudent.DateOfBirth = updatedStudent.DateOfBirth;
        existingStudent.ParentId = updatedStudent.ParentId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteStudentAsync(int studentId)
    {
        var student = await _context.Students.FindAsync(studentId);
        if (student == null) return false;

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return true;
    }
}
