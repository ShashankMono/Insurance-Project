﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using Insurance_final_project.Constant;

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
        public double AmountToBeClaimed { get; set; }
        public string ClaimDescription { get; set; }
        public string ApprovedStatus { get; set; } = ApprovalType.Pending.ToString(); // Approval status of the claim
        public DateTime DateAndTime { get; set; }
        public DateTime? AcknowledgementDate { get; set; }
    }
}
