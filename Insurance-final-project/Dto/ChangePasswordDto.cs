using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "User is required")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Old Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "new Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string NewPassword { get; set; }
    }
}
