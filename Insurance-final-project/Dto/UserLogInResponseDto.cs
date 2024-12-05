using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class UserLogInResponseDto
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(100, ErrorMessage = "Username cannot exceed 100 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Role ID is required.")]
        [ForeignKey("Role")]
        public Guid RoleId { get; set; }

        public string RoleName { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
