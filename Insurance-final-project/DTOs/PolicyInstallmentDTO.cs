using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.DTOs
{
    public class PolicyInstallmentDTO
    {
        [Required]
        public Guid PolicyAccountId { get; set; }

        [Required]
        public DateTime InstallmentDate { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Status cannot exceed 20 characters.")]
        public string Status { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public double Amount { get; set; }

        [Required]
        public Guid TransactionId { get; set; }
    }
}
