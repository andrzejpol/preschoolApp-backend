using System.ComponentModel.DataAnnotations;

namespace PreschoolApp.Requests
{
    public class RegisterUserRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

