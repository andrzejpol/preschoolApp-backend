namespace PreschoolApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using PreschoolApp.Models;
using PreschoolApp.Services.Interfaces;

[ApiController]
[Route("api/attendance")]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceService _attendanceService;

    public AttendanceController(IAttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }

    [HttpGet("{date}")]
    public async Task<IActionResult> GetAttendanceByDate([FromRoute] DateTime date)
    {
        var attendances = await _attendanceService.GetAttendanceByDateAsync(date);
        return Ok(attendances);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateAttendance([FromBody] List<Attendance> attendances)
    {
        await _attendanceService.UpdateAttendanceAsync(attendances);
        return NoContent();
    }
}
