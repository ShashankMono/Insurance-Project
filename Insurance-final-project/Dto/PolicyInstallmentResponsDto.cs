﻿using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Dto
{
    public class PolicyInstallmentResponsDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Policy Account ID is required.")]
        [ForeignKey("PolicyAccount")]
        public Guid PolicyAccountId { get; set; }

        public DateTime? InstallmentPaidDate { get; set; }

        [Required(ErrorMessage = "Installment Due Date is required.")]
        public DateTime InstallmentDueDate { get; set; }

        public bool IsPaid { get; set; } = false;

        [Range(0, double.MaxValue, ErrorMessage = "Amount cannot be negative.")]
        public double Amount { get; set; }
    }
}
