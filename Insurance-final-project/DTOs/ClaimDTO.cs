﻿using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Insurance_final_project.DTOs
{
    public class ClaimDTO
    {
        [Required]
        public Guid PolicyAccountId { get; set; }

        [Required]
        public Guid DocumentId { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal AmountToBeClaimed { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public bool ApprovedStatus { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime? AcknowledgementDate { get; set; }
    }
}
