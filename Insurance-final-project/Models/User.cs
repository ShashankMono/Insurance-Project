using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; } 
        public string Username { get; set; } 
        public string Password { get; set; }
        [ForeignKey("Role")]
        public int RoleId { get; set; } 
        public Role? Role { get; set; } //Navigation

        // IsActive property, defaulting to true only for customers
        public bool IsActive { get; set; } = true;

        public ICollection<Transaction> Transactions { get; set; } // One-to-Many relationship with Transactions
    }

}
