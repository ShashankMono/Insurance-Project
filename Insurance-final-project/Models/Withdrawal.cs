using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class Withdrawal
    {
        [Key]
        public Guid WithdrawalId { get; set; }

        public string Type { get; set; } // Commission or Cancel Policy
        public double Amount { get; set; }
        public bool Approved { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public DateTime DateAndTime { get; set; }

        [ForeignKey("PolicyAccount")]
        public Guid PolicyAccountId { get; set; }
    }
}
