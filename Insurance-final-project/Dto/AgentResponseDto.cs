using Insurance_final_project.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Dto
{
    public class AgentResponseDto
    {
        public Guid AgentId { get; set; } // Primary key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Qualification { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; } // Foreign key to User
        public User User { get; set; } // Navigation property for OTO relationship


        public ICollection<PolicyAccount>? PolicyAccounts { get; set; }  // OTM relationship
        public ICollection<Commission>? Commissions { get; set; }//OTM relationship//new
        public ICollection<CommissionWithdrawal>? CommissionWithdrawal { get; set; }//OTM relationship//new

        public double CommissionEarned { get; set; } = 0; // Total commission earned so far
        public double TotalCommission { get; set; } = 0; // Overall commission expected
    }
}
