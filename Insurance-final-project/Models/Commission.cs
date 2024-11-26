using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class Commission
    {
        [Key]
        public Guid Id { get; set; }
        public decimal Amount { get; set; } // Will be compared with commission earned
        public bool ApprovedStatus { get; set; }

        [ForeignKey("Agent")]
        public Guid AgentId { get; set; }
        public Agent Agent { get; set; }

        [ForeignKey("PolicyAccount")]
        public Guid PolicyAccountId { get; set; }
        public PolicyAccount PolicyAccount { get; set; }
    }
}
