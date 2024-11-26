using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.DTOs
{
    public class PolicyDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string PolicyDescription { get; set; }

        [Required]
        public Guid PolicyTypeId { get; set; }

        [Required]
        [Range(18, 100, ErrorMessage = "Minimum age must be between 18 and 100.")]
        public int MinimumAgeCriteria { get; set; }

        [Required]
        [Range(18, 100, ErrorMessage = "Maximum age must be between 18 and 100.")]
        public int MaximumAgeCriteria { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Investment amount must be greater than zero.")]
        public double MinimumInvestmentAmount { get; set; }

        [Required]
        public int MinimumPolicyTerm { get; set; }
        public int MaximumPolicyTerm { get; set; }

        [Required]
        public double MaximumInvestmentAmount { get; set; }

        public double ProfitPercentage { get; set; }
        public double CommissionPercentage { get; set; }
    }
}
