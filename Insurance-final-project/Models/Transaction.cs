using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }
        public string Type { get; set; } // Installment Payment or Withdrawal
        public decimal Amount { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("PolicyAccount")]
        public Guid PolicyAccountId { get; set; }
        public PolicyAccount PolicyAccount { get; set; }

        public DateTime DateTime { get; set; }
    }
}
