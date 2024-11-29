using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class CommissionWithdrawal
    {
        [Key]
        public Guid Id { get; set; }
        public double Amount { get; set; } // Will be compared with commission earned
        public bool ApprovedStatus { get; set; } = false;

        [ForeignKey("Agent")]
        public Guid AgentId { get; set; }
        public Agent Agent { get; set; }

        public bool TransactionStatus { get; set; } = false;

        //Transaction Date will be added when actual withdrwal is done and TransactionStatus will be true
        public DateTime? TransactionDate { get; set; }
    }
}
