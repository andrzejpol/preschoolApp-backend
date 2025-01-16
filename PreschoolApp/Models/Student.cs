using System.ComponentModel.DataAnnotations;

namespace PreschoolApp.Models;

public class Student
{
    public int StudentId { get; set; }
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }
    [Required]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(Student), nameof(ValidateDateOfBirth))]
    public DateTime DateOfBirth { get; set; }
    public int Age => DateTime.Today.Year - DateOfBirth.Year - 
                      (DateOfBirth.Date > DateTime.Today.AddYears(-Age) ? 1 : 0);
    [Required]
    public int ParentId { get; set; }
    public User Parent { get; set; }
    public ICollection<StudentGroup>? StudentGroups { get; set; } = new List<StudentGroup>();
    public ICollection<Review>? Reviews { get; set; } = new List<Review>();
    
    public static ValidationResult? ValidateDateOfBirth(DateTime date, ValidationContext context)
    {
        if (date > DateTime.Today)
        {
            return new ValidationResult("Date of birth must be in the past.");
        }
        return ValidationResult.Success;
    }
}