using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Insurance_final_project.Models
{
    public class Admin
    {
        [Key]
        public Guid AdminId { get; set; }  
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public User User { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

    }
}
