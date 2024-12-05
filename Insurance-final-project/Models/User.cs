using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; } 

        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(100, ErrorMessage = "Username cannot exceed 100 characters.")]
        public string Username { get; set; } 

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } 

        [Required(ErrorMessage = "Role ID is required.")]
        [ForeignKey("Role")]
        public Guid RoleId { get; set; } 

        public Role? Role { get; set; } 

        public bool IsActive { get; set; } = true;

    }

}
