﻿using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class PolicyCancelDto
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public double Amount { get; set; }

        [Required]
        public bool Approved { get; set; } = false;

        [Required]
        public DateTime DateAndTime { get; set; }

        [Required(ErrorMessage = "Policy Account ID is required.")]
        public Guid PolicyAccountId { get; set; }
    }
}
