using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Dto
{
    public class CommissionWithdrawalDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Amount cannot be negative.")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Agent ID is required.")]
        [ForeignKey("Agent")]
        public Guid AgentId { get; set; }
    }
}
