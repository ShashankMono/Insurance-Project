using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class PolicyCancelDto
    {
        public Guid PolicyCancelId { get; set; }
        [Required]
        [Range(0.00, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public double Amount { get; set; } 

        [Required]
        public string IsApproved { get; set; }

        [Required]
        public DateTime DateAndTime { get; set; }

        [Required(ErrorMessage = "Policy Account ID is required.")]
        public Guid PolicyAccountId { get; set; }
        public PolicyAccount? PolicyAccount { get; set; }
    }
}
