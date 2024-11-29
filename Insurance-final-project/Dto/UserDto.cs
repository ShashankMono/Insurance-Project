using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class UserDto
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string Username { get; set; } // Username of the user

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password cannot exceed 100 characters.")]
        public string Password { get; set; } // Password for the user

        [Required(ErrorMessage = "Role ID is required.")]
        [ForeignKey("Role")]
        public int RoleId { get; set; } // Foreign key linking to Role

        public Role Role { get; set; } // Navigation property for Role

        // IsActive property, defaulting to true only for customers
        [Required]
        public bool IsActive { get; set; } = true; // Default set to true for customers
    }
}
