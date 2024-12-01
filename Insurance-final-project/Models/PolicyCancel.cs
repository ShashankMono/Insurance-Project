using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Insurance_final_project.Constant;

namespace Insurance_final_project.Models
{
    public class PolicyCancel
    {
        [Key]
        public Guid PolicyCancelId { get; set; } // Primary Key

        public double Amount { get; set; }

        public string IsApproved { get; set; } = ApprovalType.Pending.ToString();

        public DateTime DateAndTime { get; set; } // Date and Time of cancellation

        [ForeignKey("PolicyAccount")]
        public Guid PolicyAccountId { get; set; } // Foreign Key

        public virtual PolicyAccount PolicyAccount { get; set; }
    }
}
