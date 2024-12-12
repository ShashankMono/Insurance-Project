using Insurance_final_project.Constant;
using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Dto
{
    public class ClaimDto
    {
        public Guid ClaimId { get; set; }

        [Required(ErrorMessage = "Policy Account ID is required.")]
        public Guid PolicyAccountId { get; set; }

        
        public Guid DocumentId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Amount to be claimed cannot be negative.")]
        public double AmountToBeClaimed { get; set; }

        [Required(ErrorMessage = "Claim description is required.")]
        [MaxLength(500, ErrorMessage = "Claim description cannot exceed 500 characters.")]
        public string ClaimDescription { get; set; }

        [MaxLength(50, ErrorMessage = "Approved status cannot exceed 50 characters.")]
        public string ApprovedStatus { get; set; } = ApprovalType.Pending.ToString();

        public DateTime DateAndTime { get; set; }

        public DateTime? AcknowledgementDate { get; set; }
    }
}
