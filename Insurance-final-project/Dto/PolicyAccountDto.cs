using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Dto
{
    public class PolicyAccountDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Policy ID is required.")]
        [ForeignKey("Policy")]
        public Guid PolicyId { get; set; }

        [Required(ErrorMessage = "Customer ID is required.")]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Coverage Amount cannot be negative.")]
        public double CoverageAmount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Total Amount Paid cannot be negative.")]
        public double TotalAmountPaid { get; set; }
        [Required(ErrorMessage = "Policy term is required")]
        public int PolicyTerm {  get; set; }

        [Required(ErrorMessage = "Installment Type is required.")]
        [MaxLength(50, ErrorMessage = "Installment Type cannot exceed 50 characters.")]
        public string InstallmentType { get; set; }

        [ForeignKey("Agent")]
        public Guid? AgentId { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [MaxLength(20, ErrorMessage = "Status cannot exceed 20 characters.")]
        public string Status { get; set; }

    }
}
