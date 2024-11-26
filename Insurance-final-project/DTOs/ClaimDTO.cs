using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.DTOs
{
    public class ClaimDTO
    {
        [Required]
        public Guid PolicyAccountId { get; set; }

        [Required]
        public Guid DocumentId { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount to be claimed must be greater than zero.")]
        public double AmountToBeClaimed { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string DescriptionOfClaim { get; set; }

        public bool ApprovedStatus { get; set; }
        public DateTime? AcknowledgementDate { get; set; }
    }
}
