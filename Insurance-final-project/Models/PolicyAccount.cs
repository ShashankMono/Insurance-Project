﻿using Insurance_final_project.Constant;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Models
{
    public class PolicyAccount
    {
        [Key]
        public Guid Id { get; set; } 

        [Required(ErrorMessage = "Policy ID is required.")]
        [ForeignKey("Policy")]
        public Guid PolicyId { get; set; } 

        public Policy Policy { get; set; } 

        [Required(ErrorMessage = "Customer ID is required.")]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; } 

        public Customer Customer { get; set; } 

        [Range(0, double.MaxValue, ErrorMessage = "Coverage Amount cannot be negative.")]
        public double CoverageAmount { get; set; } 

        [Range(0, double.MaxValue, ErrorMessage = "Total Amount Paid cannot be negative.")]
        public double TotalAmountPaid { get; set; }

        [Required(ErrorMessage ="Investment Amount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Investment Amount cannot be negative.")]
        public double InvestmentAmount { get; set; }

        [Required(ErrorMessage = "Policy term is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Policy term cannot be negative.")]
        public int PolicyTerm { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        public DateTime StartDate { get; set; } 

        [Required(ErrorMessage = "End Date is required.")]
        public DateTime EndDate { get; set; } 

        [Required(ErrorMessage = "Installment Type is required.")]
        [MaxLength(50, ErrorMessage = "Installment Type cannot exceed 50 characters.")]
        public string InstallmentType { get; set; } 

        [ForeignKey("Agent")]
        public Guid? AgentId { get; set; } 

        public Agent? Agent { get; set; } 

        public ICollection<PolicyInstallment>? PolicyInstallments { get; set; } 
        public ICollection<Transaction>? Transactions { get; set; } 

        [Required(ErrorMessage = "Status is required.")]
        [MaxLength(20, ErrorMessage = "Status cannot exceed 20 characters.")]
        public string Status { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Agent Commission cannot be negative.")]
        public double? AgentCommission { get; set; } = 0;
        public List<PolicyAccountDocument>? AccountDocuments { get; set; }
        //public List<Nominee>? Nominees  { get; set; }

        [Required(ErrorMessage = "Approval status is required")]

        public string IsApproved { get; set; } = ApprovalType.Pending.ToString();
    }
}
