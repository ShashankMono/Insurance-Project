using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Dto
{
    public class TransactionDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Transaction Type is required.")]
        [MaxLength(100, ErrorMessage = "Transaction Type cannot exceed 100 characters.")]
        public string Type { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Amount cannot be negative.")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Customer ID is required.")]
        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }
        [Required(ErrorMessage = "Policy Account ID is required.")]
        public Guid PolicyAccountId { get; set; }

        public Guid PolicyInstallmentId { get; set; }

        [Required(ErrorMessage = "Transaction Date and Time is required.")]
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "Reference Number is required.")]
        [MaxLength(50, ErrorMessage = "Reference Number cannot exceed 50 characters.")]
        public string ReferenceNumber { get; set; }

    }
}
