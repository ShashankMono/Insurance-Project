using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class PolicyDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "Policy name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Policy description cannot exceed 1000 characters.")]
        public string Description { get; set; }

        [Required]
        public Guid PolicyTypeId { get; set; }

        [Range(0, 150, ErrorMessage = "Minimum age criteria must be between 0 and 150.")]
        public int MinimumAgeCriteria { get; set; }

        [Range(0, 150, ErrorMessage = "Maximum age criteria must be between 0 and 150.")]
        public int MaximumAgeCriteria { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Minimum investment amount must be greater than zero.")]
        public double MinimumInvestmentAmount { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Minimum policy term must be at least 1 year.")]
        public int MinimumPolicyTerm { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Maximum policy term must be at least 1 year.")]
        public int MaximumPolicyTerm { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Maximum investment amount must be greater than zero.")]
        public double MaximumInvestmentAmount { get; set; }

        [Range(0.01, 100, ErrorMessage = "Profit percentage must be between 0.01 and 100.")]
        public double ProfitPercentage { get; set; }

        [Range(0.01, 100, ErrorMessage = "Commission percentage must be between 0.01 and 100.")]
        public double CommissionPercentage { get; set; }
    }
}
