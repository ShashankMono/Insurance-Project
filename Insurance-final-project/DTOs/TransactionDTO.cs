using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.DTOs
{
    public class TransactionDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "Transaction type cannot exceed 50 characters.")]
        public string Type { get; set; } // Installment Payment or Withdrawal

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Transaction amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid PolicyAccountId { get; set; }

        public DateTime DateTime { get; set; }
    }
}
