using System.ComponentModel.DataAnnotations;

namespace Insurance_final_project.Models
{
    public class Role
    {
        [Key]
        public Guid RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
