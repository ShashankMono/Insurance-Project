using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class CommissionWithdrawalDto
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Commission amount must be greater than zero.")]
        public double Amount { get; set; }

        public bool ApprovedStatus { get; set; } = false;
        public bool TransactionStatus { get; set; } = false;
        public DateTime ? TransactionDate { get; set; }
        [Required]
        public Guid AgentId { get; set; }
    }
}
