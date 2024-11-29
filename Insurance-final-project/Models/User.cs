using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; } 
        public string Username { get; set; } 
        public string Password { get; set; }
        [ForeignKey("Role")]
        public Guid RoleId { get; set; } 
        public Role Role { get; set; } //Navigation

        // IsActive property, defaulting to true only for customers
        public bool IsActive { get; set; } = true;

        
    }

}
