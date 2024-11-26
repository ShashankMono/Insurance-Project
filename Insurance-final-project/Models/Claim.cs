using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class Claim
    {
        [Key]
        public Guid ClaimId { get; set; }

        [ForeignKey("PolicyAccount")]
        public Guid PolicyAccountId { get; set; }

        [ForeignKey("Document")]
        public Guid DocumentId { get; set; }

        public double AmountToBeClaimed { get; set; }
        public string DescriptionOfClaim { get; set; }
        public bool ApprovedStatus { get; set; } // True if approved
        public DateTime DateAndTime { get; set; }
        public DateTime? AcknowledgementDate { get; set; }
    }
}
