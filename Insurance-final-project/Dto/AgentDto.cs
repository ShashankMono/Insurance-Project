namespace Insurance_final_project.Dto
{
    using Insurance_final_project.Models;
    using System.ComponentModel.DataAnnotations;

    public class AgentDto
    {
        public Guid AgentId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string AgentFirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string AgentLastName { get; set; } = string.Empty;

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
        public int UserId { get; set; } // Foreign key to User
        public User? User { get; set; } // Navigation property for OTO relationship

        public ICollection<Customer>? Customers { get; set; } = new List<Customer>(); // OTM relationship

        public ICollection<PolicyAccount>? PolicyAccounts { get; set; } = new List<PolicyAccount>(); // OTM relationship

        [Range(0, double.MaxValue, ErrorMessage = "Commission earned must be a non-negative value.")]
        public decimal CommissionEarned { get; set; } // Total commission earned so far

        [Range(0, double.MaxValue, ErrorMessage = "Total commission must be a non-negative value.")]
        public decimal TotalCommission { get; set; } // Overall commission expected

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(20, ErrorMessage = "Status cannot exceed 20 characters.")]
        public string Status { get; set; } = "Active"; // Status of the agent
    }

}
