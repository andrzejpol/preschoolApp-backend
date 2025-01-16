namespace PreschoolApp.Models;

public class User: BaseEntity
{
    public enum UserRole
    {
        Admin,
        Teacher,
        Parent,
        Guest
    }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PasswordHash { get; set; }
    public byte[] Salt { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; } = UserRole.Guest;
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? City { get; set; }
    public string? TaxNumber { get; set; }
    public string? ZipCode { get; set; }
    public string? Country { get; set; }
    
    public ICollection<Student>? Children { get; set; } = new List<Student>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<TeacherSubject>? TeacherSubjects { get; set; }
    public ICollection<TeacherGroup>? TeacherGroups { get; set; }
    public ICollection<Message> SentMessages { get; set; } = new List<Message>();
    public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
    public DateTime? CreatedAt { get; set; }
}