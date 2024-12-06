namespace Insurance_final_project.Dto
{
    using Insurance_final_project.Models;
    using System.ComponentModel.DataAnnotations;

    public class AgentInputDto
    {
        public Guid AgentId { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Qualification is required.")]
        [StringLength(100, ErrorMessage = "Qualification cannot exceed 100 characters.")]
        public string Qualification { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mobile number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [MaxLength(10, ErrorMessage = "Mobile Number cannot exceed 10 characters.")]
        public string MobileNo { get; set; } = string.Empty;

        public Guid? UserId { get; set; } // Foreign key to User

        [Range(0, double.MaxValue, ErrorMessage = "Commission earned must be a non-negative value.")]
        public double? CommissionEarned { get; set; } = 0; // Total commission earned so far

        [Range(0, double.MaxValue, ErrorMessage = "Total commission must be a non-negative value.")]
        public double? TotalCommission { get; set; } = 0; // Overall commission expected



    }

}
