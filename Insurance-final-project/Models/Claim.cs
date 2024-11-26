using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Insurance_final_project.Models
{
    public class Claim
    {
        [Key]
        public Guid ClaimId { get; set; }

        [Required]
        [ForeignKey("PolicyAccount")]
        public Guid PolicyAccountId { get; set; }
        public PolicyAccount PolicyAccount { get; set; }

        [ForeignKey("Document")]
        public Guid? DocumentId { get; set; }
        public Document Document { get; set; }

        public decimal AmountToBeClaimed { get; set; }
        public string ClaimDescription { get; set; }
        public bool ApprovedStatus { get; set; } // Approval status of the claim
        public DateTime DateAndTime { get; set; }
        public DateTime? AcknowledgementDate { get; set; }
    }
}
