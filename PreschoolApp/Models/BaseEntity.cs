using System.ComponentModel.DataAnnotations;

namespace PreschoolApp.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
    
