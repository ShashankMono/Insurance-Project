﻿using Insurance_final_project.Constant;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class PolicyCancelReponseDto
    {
        public Guid PolicyCancelId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Amount cannot be negative.")]
        public double Amount { get; set; }

        [Required(ErrorMessage = "Approval status is required.")]
        public string IsApproved { get; set; } = ApprovalType.Pending.ToString();

        [Required(ErrorMessage = "Date and Time of cancellation is required.")]
        public DateTime DateAndTime { get; set; }

        [Required(ErrorMessage = "Policy Account ID is required.")]
        public Guid PolicyAccountId { get; set; }

        public string? policyName { get; set; }

        public string? customerName { get; set; }
    }
}
