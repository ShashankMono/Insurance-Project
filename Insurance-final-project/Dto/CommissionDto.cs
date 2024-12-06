using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Dto
{
    public class CommissionDto
    {
        public Guid CommissionId { get; set; }

        [Required(ErrorMessage = "Commission Type is required.")]
        [MaxLength(50, ErrorMessage = "Commission Type cannot exceed 50 characters.")]
        public string CommissionType { get; set; }

        [Required(ErrorMessage = "Agent ID is required.")]
        public Guid AgentId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Amount cannot be negative.")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Policy Account ID is required.")]
        public Guid PolicyAccountId { get; set; }
    }
}
