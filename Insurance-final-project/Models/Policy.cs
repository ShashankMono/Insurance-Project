using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Insurance_final_project.Models
{
    public class Policy
    {
        [Key]
        public Guid Id { get; set; } 

        [Required(ErrorMessage = "Policy Name is required.")]
        [MaxLength(100, ErrorMessage = "Policy Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Policy Description is required.")]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image url is required.")]
        [Url(ErrorMessage = "Invalid image URL format.")]
        public string ImageUrl { get; set; } 

        [Required(ErrorMessage = "Policy Type ID is required.")]
        [ForeignKey("PolicyType")]
        public Guid PolicyTypeId { get; set; } 

        public PolicyType PolicyType { get; set; }
        [Required(ErrorMessage = "Minimum age is required.")]

        [Range(18, 100, ErrorMessage = "Minimum Age Criteria must be between 18 and 100.")]
        public int MinimumAgeCriteria { get; set; }

        [Required(ErrorMessage = "Maximum age is required.")]
        [Range(18, 100, ErrorMessage = "Maximum Age Criteria must be between 18 and 100.")]
        public int MaximumAgeCriteria { get; set; }

        [Required(ErrorMessage = "Minimum Investment amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Minimum Investment Amount cannot be negative.")]
        public double MinimumInvestmentAmount { get; set; }

        [Required(ErrorMessage = "Minimum policy term is required.")]
        [Range(1, 100, ErrorMessage = "Minimum Policy Term must be between 1 and 100 years.")]
        public int MinimumPolicyTerm { get; set; }

        [Required(ErrorMessage = "Maximum policy term is required.")]
        [Range(1, 100, ErrorMessage = "Maximum Policy Term must be between 1 and 100 years.")]
        public int MaximumPolicyTerm { get; set; }

        [Required(ErrorMessage = "Maximum investment amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Maximum Investment Amount cannot be negative.")]
        public double MaximumInvestmentAmount { get; set; }

        [Required(ErrorMessage = "Profit precentage is required.")]
        [Range(0, 100, ErrorMessage = "Profit Percentage must be between 0 and 100.")]
        public double ProfitPercentage { get; set; }

        [Required(ErrorMessage = "Comission percentage is required.")]
        [Range(0, 100, ErrorMessage = "Commission Percentage must be between 0 and 100.")]
        public double CommissionPercentage { get; set; }

        //[Required(ErrorMessage = "Document is required is required.")]  
        //[MaxLength(100,ErrorMessage ="Document can be of max 100 characters")]
        //public string DocumentsRequired { get; set; }
        public ICollection<PolicyAccount>? PolicyAccounts { get; set; } 

        public bool IsActive { get; set; } = true; 
    }
}
