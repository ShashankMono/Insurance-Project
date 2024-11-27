using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.DTOs
{
    public class PolicyCancelDTO
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public double Amount { get; set; }

        [Required]
        public bool Approved { get; set; }

        [Required]
        public DateTime DateAndTime { get; set; }

        [Required(ErrorMessage = "Policy Account ID is required.")]
        public int PolicyAccountId { get; set; }
    }
}
