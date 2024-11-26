using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.DTOs
{
    public class WithdrawalDTO
    {
        [Required]
        public string Type { get; set; } // Commission or Cancel Policy

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        public bool Approved { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid PolicyAccountId { get; set; }

        public DateTime DateTime { get; set; }
    }
}
