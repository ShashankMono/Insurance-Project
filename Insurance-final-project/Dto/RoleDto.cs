using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class RoleDto
    {
        [Key]
        public Guid RoleId { get; set; }

        [Required(ErrorMessage = "Role Name is required.")]
        [MaxLength(100, ErrorMessage = "Role Name cannot exceed 100 characters.")]
        public string RoleName { get; set; }
    }
}
