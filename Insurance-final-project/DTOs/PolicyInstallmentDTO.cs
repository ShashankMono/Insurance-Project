using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations;


namespace Insurance_final_project.DTOs
{
    public class PolicyInstallmentDTO
    {
        public PolicyAccount PolicyAccount { get; set; }
        [Required]
        public Guid PolicyAccountId { get; set; }
        [Required]
        public DateTime InstallmentPaidDate { get; set; }
        [Required]
        public DateTime InstallmentDueDate { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Status cannot exceed 20 characters.")]
        public string Status { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public double Amount { get; set; }
        
    }
}
