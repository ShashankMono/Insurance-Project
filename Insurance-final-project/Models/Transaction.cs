using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }

        public string Type { get; set; } // Installment Payment, Commission, or Claim Withdrawal
        public decimal Amount { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("PolicyAccount")]
        public Guid PolicyAccountId { get; set; }

        public DateTime DateAndTime { get; set; }
    }
}
