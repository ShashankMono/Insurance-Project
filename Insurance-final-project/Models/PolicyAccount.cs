using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Models
{
    public class PolicyAccount
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Policy")]
        public Guid PolicyId { get; set; }
        public Policy Policy { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public double CoverageAmount { get; set; }
        public double TotalAmountPaid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string InstallmentType { get; set; } // Monthly, Quarterly, etc.

        [ForeignKey("Agent")]
        public Guid AgentId { get; set; }
        public Agent Agent { get; set; }

        public string Status { get; set; } // Open/Closed
        public double AgentCommission { get; set; } // Calculated based on installment payments and commission percentage
    }
}
