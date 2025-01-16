using Microsoft.AspNetCore.Mvc;
using PreschoolApp.Models;
using PreschoolApp.Services.Interfaces;

namespace PreschoolApp.Controllers;

[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        var students = await _studentService.GetAllStudentsAsync();
        return Ok(students);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudent(int id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);
        if (student == null) return NotFound();
        return Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> AddStudent([FromBody] Student newStudent)
    {
        var student = await _studentService.AddStudentAsync(newStudent);
        return CreatedAtAction(nameof(GetStudent), new { id = student.StudentId }, student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student updatedStudent)
    {
        var success = await _studentService.UpdateStudentAsync(id, updatedStudent);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var success = await _studentService.DeleteStudentAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
