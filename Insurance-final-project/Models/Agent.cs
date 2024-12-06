using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Models
{
    public class Agent
    {
        [Key]
        public Guid AgentId { get; set; } 

        [Required(ErrorMessage = "First Name is required.")]
        [MaxLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [MaxLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Qualification is required.")]
        [MaxLength(100, ErrorMessage = "Qualification cannot exceed 100 characters.")]
        public string Qualification { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [MaxLength(10, ErrorMessage = "Mobile Number cannot exceed 10 characters.")]
        public string MobileNo { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; } 
        public User User { get; set; } 

        public ICollection<PolicyAccount>? PolicyAccounts { get; set; } 
        public ICollection<Commission>? Commissions { get; set; } 
        public ICollection<CommissionWithdrawal>? CommissionWithdrawals { get; set; } 

        [Range(0, double.MaxValue, ErrorMessage = "Commission Earned cannot be negative.")]
        public double CommissionEarned { get; set; } = 0; 

        [Range(0, double.MaxValue, ErrorMessage = "Total Commission cannot be negative.")]
        public double TotalCommission { get; set; } = 0; 


    }
}
