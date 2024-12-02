using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class PolicyInstallment
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("PolicyAccount")]
        public Guid PolicyAccountId { get; set; }
        public PolicyAccount PolicyAccount { get; set; }
        public DateTime? InstallmentPaidDate { get; set; }
        public DateTime InstallmentDueDate { get; set; }
        public bool IsPaid { get; set; } = false; // Paid or Pending
        public double Amount { get; set; }
        [ForeignKey("Transaction")]
        public Guid? TransactionId { get; set; }
        public Transaction? Transaction { get; set; }

    }
}
