namespace PreschoolApp.DTO;

public class GroupDTO
{
    public string GroupName { get; set; }
    public string Id { get; set; }
    public List<int> Supervisors { get; set; } = new List<int>();
}