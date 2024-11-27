using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class PolicyCancel
    {
        [Key]
        public int PolicyCancelId { get; set; } // Primary Key

        public double Amount { get; set; }

        public bool Approved { get; set; }

        public DateTime DateAndTime { get; set; } // Date and Time of cancellation

        [ForeignKey("PolicyAccount")]
        public int PolicyAccountId { get; set; } // Foreign Key

        public virtual PolicyAccount PolicyAccount { get; set; }
    }
}
