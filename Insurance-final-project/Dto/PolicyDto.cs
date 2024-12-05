using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Dto
{
    public class PolicyDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Policy Name is required.")]
        [MaxLength(100, ErrorMessage = "Policy Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Policy Description is required.")]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Url(ErrorMessage = "Invalid image URL format.")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Policy Type ID is required.")]
        [ForeignKey("PolicyType")]
        public Guid PolicyTypeId { get; set; }

        [Range(18, 100, ErrorMessage = "Minimum Age Criteria must be between 18 and 100.")]
        public int MinimumAgeCriteria { get; set; }

        [Range(18, 100, ErrorMessage = "Maximum Age Criteria must be between 18 and 100.")]
        public int MaximumAgeCriteria { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Minimum Investment Amount cannot be negative.")]
        public double MinimumInvestmentAmount { get; set; }

        [Range(1, 100, ErrorMessage = "Minimum Policy Term must be between 1 and 100 years.")]
        public int MinimumPolicyTerm { get; set; }

        [Range(1, 100, ErrorMessage = "Maximum Policy Term must be between 1 and 100 years.")]
        public int MaximumPolicyTerm { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Maximum Investment Amount cannot be negative.")]
        public double MaximumInvestmentAmount { get; set; }

        [Range(0, 100, ErrorMessage = "Profit Percentage must be between 0 and 100.")]
        public double? ProfitPercentage { get; set; }

        [Range(0, 100, ErrorMessage = "Commission Percentage must be between 0 and 100.")]
        public double CommissionPercentage { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
