namespace PreschoolApp.Models;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<TeacherGroup> TeacherGroups { get; set; } = new List<TeacherGroup>();

    public ICollection<StudentGroup> StudentGroups { get; set; } = new List<StudentGroup>();
}