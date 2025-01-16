namespace PreschoolApp.Models;

public class Attendance
{
    public int AttendanceId { get; set; }
    public int ChildId { get; set; }
    public Student Student { get; set; }
    public DateTime AttendanceDate { get; set; }
    public bool IsPresent { get; set; }
}