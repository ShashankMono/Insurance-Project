using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.DTOs
{
    public class CommissionDTO
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Commission amount must be greater than zero.")]
        public decimal Amount { get; set; }

        public bool ApprovedStatus { get; set; }

        [Required]
        public Guid AgentId { get; set; }

        [Required]
        public Guid PolicyAccountId { get; set; }
    }
}
