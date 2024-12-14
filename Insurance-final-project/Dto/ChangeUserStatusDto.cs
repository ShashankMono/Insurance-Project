using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class ChangeUserStatusDto
    {
        [Required(ErrorMessage="User is required")]
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public bool IsActive { get; set; }
    }
}
