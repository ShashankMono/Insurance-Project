using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class UserUpdateDto
    {
        [Required(ErrorMessage = "User Id is required.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [MaxLength(100, ErrorMessage = "Username cannot exceed 100 characters.")]
        public string Username { get; set; }
    }
}
