using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class PolicyAccountDto
    {
        [Required]
        public Guid PolicyId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Coverage amount must be greater than zero.")]
        public double CoverageAmount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Total amount paid must be non-negative.")]
        public double TotalAmountPaid { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Installment type cannot exceed 50 characters.")]
        public string InstallmentType { get; set; } // Monthly, Quarterly, etc.

        [Required]
        public Guid AgentId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string Status { get; set; } // Open or Closed

        [Range(0, double.MaxValue, ErrorMessage = "Agent commission must be non-negative.")]
        public double AgentCommission { get; set; }
    }
}
