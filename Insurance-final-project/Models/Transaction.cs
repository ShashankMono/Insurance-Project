using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }
        public string Type { get; set; } // Installment Payment or Withdrawal // getting string from fronend from selectors
        public double Amount { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("PolicyAccount")]
        public Guid PolicyAccountId { get; set; }
        public PolicyAccount PolicyAccount { get; set; }

        public DateTime DateTime { get; set; }
        public string ReferenceNumber { get; set; }
    }
}
