namespace PreschoolApp.Services.Interfaces;

using PreschoolApp.Models;

public interface IAttendanceService
{
    Task<List<Attendance>> GetAttendanceByDateAsync(DateTime date);
    Task UpdateAttendanceAsync(List<Attendance> attendances);
}