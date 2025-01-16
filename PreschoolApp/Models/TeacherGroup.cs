namespace PreschoolApp.Models;

public class TeacherGroup
{
    public int TeacherId { get; set; }
    public User Teacher { get; set; }

    public int GroupId { get; set; }
    public Group Group { get; set; }
}