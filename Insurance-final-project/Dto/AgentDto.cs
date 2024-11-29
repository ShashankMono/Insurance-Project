namespace Insurance_final_project.Dto
{
    using Insurance_final_project.Models;
    using System.ComponentModel.DataAnnotations;

    public class AgentDto
    {

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
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Invalid mobile number format.")]
        public string MobileNo { get; set; } = string.Empty;

        [Required]
        public Guid UserId { get; set; } // Foreign key to User

        [Range(0, double.MaxValue, ErrorMessage = "Commission earned must be a non-negative value.")]
        public double CommissionEarned { get; set; } // Total commission earned so far

        [Range(0, double.MaxValue, ErrorMessage = "Total commission must be a non-negative value.")]
        public double TotalCommission { get; set; } // Overall commission expected

        //[Required(ErrorMessage = "Status is required.")]
        //[StringLength(20, ErrorMessage = "Status cannot exceed 20 characters.")]
        //public bool IsActive { get; set; } = true; // Status of the agent
    }

}
