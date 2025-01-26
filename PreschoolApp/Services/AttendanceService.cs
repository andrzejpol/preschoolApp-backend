using PreschoolApp.Services.Interfaces;

namespace PreschoolApp.Services;

using Microsoft.EntityFrameworkCore;
using PreschoolApp.Data;
using PreschoolApp.Models;

public class AttendanceService : IAttendanceService
{
    private readonly PreschoolDbContext _context;

    public AttendanceService(PreschoolDbContext context)
    {
        _context = context;
    }

    public async Task<List<Attendance>> GetAttendanceByDateAsync(DateTime date)
    {
        return await _context.Attendances
            .Include(a => a.Student)
            .Where(a => a.AttendanceDate.Date == date.Date)
            .ToListAsync();
    }

    public async Task UpdateAttendanceAsync(List<Attendance> attendances)
    {
        foreach (var attendance in attendances)
        {
            var existing = await _context.Attendances
                .FirstOrDefaultAsync(a => a.AttendanceId == attendance.AttendanceId);

            if (existing != null)
            {
                existing.IsPresent = attendance.IsPresent;
            }
            else
            {
                _context.Attendances.Add(attendance);
            }
        }

        await _context.SaveChangesAsync();
    }
}
