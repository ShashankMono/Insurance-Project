using Insurance_final_project.Constant;
using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class ClaimDto
    {
        public Guid ClaimId { get; set; }
        [Required]
        public Guid PolicyAccountId { get; set; }
        public PolicyAccount PolicyAccount { get; set; }

        [Required]
        public Guid DocumentId { get; set; }
        public Document Document { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public double AmountToBeClaimed { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public string ApprovedStatus { get; set; } = ApprovalType.Pending.ToString();
        public DateTime DateTime { get; set; }
        public DateTime? AcknowledgementDate { get; set; }
    }
}
