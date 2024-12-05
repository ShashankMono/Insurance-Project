using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Models
{
    public class Admin
    {
        [Key]
        public Guid AdminId { get; set; } 

        [Required(ErrorMessage = "First Name is required.")]
        [MaxLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; } 

        [Required(ErrorMessage = "Last Name is required.")]
        [MaxLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; } 

        [Required(ErrorMessage = "User ID is required.")]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}
