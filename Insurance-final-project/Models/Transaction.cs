using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; } 

        [Required(ErrorMessage = "Transaction Type is required.")]
        [MaxLength(100, ErrorMessage = "Transaction Type cannot exceed 100 characters.")]
        public string Type { get; set; } 

        [Range(0, double.MaxValue, ErrorMessage = "Amount cannot be negative.")]
        public double Amount { get; set; } 

        [Required(ErrorMessage = "Customer ID is required.")]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; } 

        public Customer Customer { get; set; } 
        [Required(ErrorMessage = "Policy Account ID is required.")]
        [ForeignKey("PolicyAccount")]
        public Guid PolicyAccountId { get; set; } 

        public PolicyAccount PolicyAccount { get; set; } 

        [ForeignKey("PolicyInstallment")]
        public Guid? PolicyInstallmentId { get; set; } 

        public PolicyInstallment? PolicyInstallment { get; set; } 

        [Required(ErrorMessage = "Transaction Date and Time is required.")]
        public DateTime DateTime { get; set; } 

        [Required(ErrorMessage = "Reference Number is required.")]
        [MaxLength(50, ErrorMessage = "Reference Number cannot exceed 50 characters.")]
        public Guid ReferenceNumber { get; set; }
    }
}
