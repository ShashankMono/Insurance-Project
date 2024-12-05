using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Insurance_final_project.Constant;

namespace Insurance_final_project.Models
{
    public class PolicyCancel
    {
        [Key]
        public Guid PolicyCancelId { get; set; } 

        [Range(0, double.MaxValue, ErrorMessage = "Amount cannot be negative.")]
        public double Amount { get; set; } 

        [Required(ErrorMessage = "Approval status is required.")]
        public string IsApproved { get; set; } = ApprovalType.Pending.ToString();

        [Required(ErrorMessage = "Date and Time of cancellation is required.")]
        public DateTime DateAndTime { get; set; } 

        [Required(ErrorMessage = "Policy Account ID is required.")]
        [ForeignKey("PolicyAccount")]
        public Guid PolicyAccountId { get; set; }

        public PolicyAccount PolicyAccount { get; set; }
    }
}
