using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class CommissionWithdrawal
    {
        [Key]
        public Guid Id { get; set; }
        public double Amount { get; set; } // Will be compared with commission earned
        public bool ApprovedStatus { get; set; }

        [ForeignKey("Agent")]
        public Guid AgentId { get; set; }
        public Agent Agent { get; set; }

        public string TransactionStatus { get; set; }
    }
}
