﻿using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.DTOs
{
    public class TransactionDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "Type cannot exceed 50 characters.")]
        public string Type { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid PolicyAccountId { get; set; }
    }
}
