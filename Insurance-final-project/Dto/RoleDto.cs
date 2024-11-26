using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class RoleDto
    {

        [Required(ErrorMessage ="The role name is required field")]
        public string RoleName { get; set; }
    }
}
