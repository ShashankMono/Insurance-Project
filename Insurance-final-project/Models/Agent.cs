using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Models
{
    public class Agent
    {
        [Key]
        public Guid AgentId { get; set; } // Primary key
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Qualification { get; set; } 
        public string Email { get; set; } 
        public string MobileNo { get; set; } 

        [ForeignKey("User")]
        public Guid UserId { get; set; } // Foreign key to User
        public User? User { get; set; } // Navigation property for OTO relationship

        public ICollection<Customer>? Customers { get; set; }  // MTM relationship

        public ICollection<PolicyAccount>? PolicyAccounts { get; set; }  // OTM relationship

        public double CommissionEarned { get; set; } // Total commission earned so far
        public double TotalCommission { get; set; } // Overall commission expected
        //public bool IsActive { get; set; } = true; // Status of the agent

    }
}
