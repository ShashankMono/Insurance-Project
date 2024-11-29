using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class PolicyCancel
    {
        [Key]
        public Guid PolicyCancelId { get; set; } // Primary Key

        public double Amount { get; set; }

        public bool Approved { get; set; }=false;

        public DateTime DateAndTime { get; set; } // Date and Time of cancellation

        [ForeignKey("PolicyAccount")]
        public Guid PolicyAccountId { get; set; } // Foreign Key

        public virtual PolicyAccount PolicyAccount { get; set; }
    }
}
