using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class AgentResponseDto
    {
        public Guid AgentId { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Qualification is required.")]
        [StringLength(100, ErrorMessage = "Qualification cannot exceed 100 characters.")]
        public string Qualification { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [MaxLength(10, ErrorMessage = "Mobile Number cannot exceed 10 characters.")]
        public string MobileNo { get; set; }

        public Guid UserId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Commission earned must be a non-negative value.")]
        public double CommissionEarned { get; set; } = 0;

        [Range(0, double.MaxValue, ErrorMessage = "Total commission must be a non-negative value.")]
        public double TotalCommission { get; set; } = 0;
    }
}
