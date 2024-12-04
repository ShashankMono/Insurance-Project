using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class RoleDto
    {
        public Guid RoleId { get; set; }
        [Required(ErrorMessage ="The role name is required field")]
        public string RoleName { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}
