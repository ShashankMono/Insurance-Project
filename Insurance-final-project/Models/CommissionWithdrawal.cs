using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Insurance_final_project.Constant;

namespace Insurance_final_project.Models
{
    public class CommissionWithdrawal
    {
        [Key]
        public Guid Id { get; set; } 

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Amount cannot be negative.")]
        public double Amount { get; set; } 

        [Required(ErrorMessage = "Agent ID is required.")]
        [ForeignKey("Agent")]
        public Guid AgentId { get; set; } 
        public Agent Agent { get; set; } 

        public DateTime TransactionDate { get; set; }
    }
}
