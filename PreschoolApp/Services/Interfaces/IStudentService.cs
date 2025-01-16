using PreschoolApp.Models;

namespace PreschoolApp.Services.Interfaces;

public interface IStudentService
{
    Task<List<Student>> GetAllStudentsAsync();
    Task<Student> GetStudentByIdAsync(int studentId);
    Task<Student> AddStudentAsync(Student newStudent);
    Task<bool> UpdateStudentAsync(int studentId, Student updatedStudent);
    Task<bool> DeleteStudentAsync(int studentId);
}