using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class AdminDto
    {
        public Guid AdminId { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        public Guid UserId { get; set; }
    }
}
