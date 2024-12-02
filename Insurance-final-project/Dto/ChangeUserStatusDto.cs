using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class ChangeUserStatusDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
